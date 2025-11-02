using System;
using System.Collections.Concurrent;

/// <summary>
/// NumberQueueService works as two things:
/// - an enqueuer: external callers enqueue requests to assign a bib number to an AthleteId
/// - a background processor: it dequeues requests and assigns numbers atomically using the DB
///
/// This avoids race conditions when many concurrent registrations occur.
/// </summary>
namespace ProyectoPuntoNET.Data
{ 
    public class NumberQueueService : BackgroundService
    {
        private readonly ConcurrentQueue<Func<IServiceProvider, Task>> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);
        private readonly IServiceProvider _services;


        public NumberQueueService(IServiceProvider services)
        {
            _services = services;
        }


        /// <summary>
        /// Enqueue an action that will run in the background with a scoped provider (so it can use DbContext)
        /// The action receives IServiceProvider and should perform DB updates.
        /// </summary>
        public void Enqueue(Func<IServiceProvider, Task> workItem)
        {
            _queue.Enqueue(workItem);
            _signal.Release();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _signal.WaitAsync(stoppingToken);


                if (_queue.TryDequeue(out var workItem))
                {
                    try
                    {
                        using var scope = _services.CreateScope();
                        await workItem(scope.ServiceProvider);
                    }
                    catch (Exception ex)
                    {
                        // TODO: add logging
                        Console.WriteLine("Error processing queue item: " + ex);
                    }
                }
            }
        }
    }
 }
 