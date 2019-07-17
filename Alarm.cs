using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using QuickStart.DisplayHelpers;
using QuickStart.Infrastructure;

namespace QuickStart
{
	public class Alarm : Program
	{
		private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

		public Alarm(ConfiguredProperties properties) : base(properties) {}

		protected override bool RequireDistinctUserAccounts
		{
			get { return true; }
		}

		protected override int Execute(DateTime when)
		{
			int result;
			PostAssetRequest load = SampleFactory.BuildLoad(when);
			PostAssetRequest truckWithAlarm = SampleFactory.BuildTruckWithAlarm(load);
			DateTime deadline = DateTime.Now + Timeout;

			SessionFacade session1;
			SessionFacade session2;
			if (Account1FailsLogin(out session1) || Account2FailsLogin(out session2))
			{
				result = Result.Failure;
			}
			else
			{
				session1.DeleteAllAssets();
				string alarmUrl = AlarmUrl.AbsoluteUri + "/";
				session1.UpdateAlarm(alarmUrl);
				session1.Post(truckWithAlarm);

				// listener implements IDisposable to allow cleanup of HTTP ports
				using (var listener = new Listener(AlarmUrl, deadline))
				{
					var cancellableWaitForAlarm = new CancellationTokenSource();

					// task for getting the listener started
					Task startListener = Task.Factory.StartNew(() => listener.Start());

					// task for returning an HTTP request, if any
					Task<HttpListenerRequest> receiveAlarm = startListener.ContinueWith(p => listener.GetRequest(),
					                                                                    cancellableWaitForAlarm.Token);

					// if task completes before timeout, post a search that should trigger the alarm
					if (startListener.Wait(Timeout))
					{
						if (startListener.IsCanceled)
						{
							Console.WriteLine("Background task canceled before listening began.");
							return Result.Failure;
						}
						if (startListener.IsFaulted)
						{
							Console.WriteLine("Background task faulted before listening began.");
							return Result.Failure;
						}
						if (!startListener.IsCompleted)
						{
							Console.WriteLine("Unspecified error with starting listener in background task.");
							return Result.Failure;
						}
						Console.WriteLine("Background task is listening for alarm.");
						session2.Post(load);
					}
					else
					{
						Console.WriteLine("Background listener was not ready within {0}ms.", Timeout.TotalMilliseconds);
						cancellableWaitForAlarm.Cancel();
						return Result.Failure;
					}

					// if receive an alarm before the timeout, print it to console
					if (receiveAlarm.Wait(Timeout))
					{
						if (receiveAlarm.IsCanceled)
						{
							Console.WriteLine("Background task canceled before any alarm was received.");
							return Result.Failure;
						}
						if (receiveAlarm.IsFaulted)
						{
							Console.WriteLine("Background task faulted before any alarm was received.");
							return Result.Failure;
						}
						if (!receiveAlarm.IsCompleted)
						{
							Console.WriteLine("Unspecified error with listener awaiting alarm in background task.");
							return Result.Failure;
						}
						Console.WriteLine("Background task completed.");
						HttpListenerRequest request = receiveAlarm.Result;
						Console.WriteLine("HTTP requested host name and port: " + request.UserHostName);
						Console.WriteLine("HTTP requested IP address and port: " + request.UserHostAddress);
						Console.WriteLine("HTTP raw url: " + request.RawUrl);
						Console.WriteLine("HTTP method: " + request.HttpMethod);
						Console.WriteLine("HTTP content type: " + request.ContentType);
						if (request.HasEntityBody)
						{
							Console.WriteLine("HTTP body: ");
							using (var streamReader = new StreamReader(request.InputStream))
							{
								Console.WriteLine(streamReader.ReadToEnd());
							}
						}
						Console.WriteLine("IP address and port of requester: " + request.RemoteEndPoint);
					}
					else
					{
						Console.WriteLine("Background task timed out after {0}ms.", Timeout.TotalMilliseconds);
						return Result.Failure;
					}
				}

				result = Result.Success;
			}
			return result;
		}

		protected override int Validate()
		{
			if (HttpListener.IsSupported == false)
			{
				Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
				return Result.Invalid;
			}
			return base.Validate();
		}

		public class Listener : IDisposable
		{
			private HttpListener _httpListener;
			private bool _disposed;
			private readonly DateTime _deadline;

			public Listener(Uri alarmUrl, DateTime deadline)
			{
				_deadline = deadline;
				_httpListener = new HttpListener();
				string uriPrefix = alarmUrl.AbsoluteUri;
				if (uriPrefix.ClipRight(1) != "/")
				{
					uriPrefix += "/";
				}
				_httpListener.Prefixes.Add(uriPrefix);
			}

			~Listener()
			{
				Dispose(false);
			}

			public void Start()
			{
				EnsureNotDisposed();

				_httpListener.Start();

				while (_httpListener.IsListening == false)
				{
					if (DeadlineExpired())
					{
						return;
					}
				}
			}

			private bool DeadlineExpired()
			{
				return DateTime.Now >= _deadline;
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			private void Dispose(bool disposing)
			{
				if (_disposed)
				{
					return;
				}
				if (_httpListener != null)
				{
					if (_httpListener.IsListening)
					{
						_httpListener.Abort();
						_httpListener = null;
					}
				}
				_disposed = true;
			}

			public HttpListenerRequest GetRequest()
			{
				EnsureNotDisposed();
				// block until request arrives
				HttpListenerContext httpListenerContext = _httpListener.GetContext();
				return httpListenerContext.Request;
			}


			private void EnsureNotDisposed()
			{
				if (_disposed)
				{
					throw new ObjectDisposedException(GetType().Name);
				}
			}
		}
	}
}