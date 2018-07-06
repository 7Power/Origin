using Shaiya.Origin.Common.Logging;
using System.Collections.Generic;
using System.Threading;

namespace Shaiya.Origin.Game.World.Pulse
{
    /// <summary>
    /// The game pulse handler, which is used to process game world tasks.
    /// </summary>
    public class GamePulseHandler
    {
        private readonly object _syncObjectOffer = new object();
        private readonly object _syncObjectPulse = new object();
        private Queue<Task.Task> _tasks = new Queue<Task.Task>();
        private bool _isRunning = false;

        /// <summary>
        /// Start the game pulse handler.
        /// </summary>
        public void Start()
        {
            if (_isRunning)
            {
                Logger.Info("Game pulse handler is already running.");
                return;
            }

            _isRunning = true;

            Thread thread = new Thread(Pulse);
            Thread playerUpdating = new Thread(PulsePlayerUpdates);
            Thread mobUpdating = new Thread(PulseMobUpdates);

            thread.Start();
            playerUpdating.Start();
            mobUpdating.Start();
        }

        /// <summary>
        /// Begin processing game tasks.
        /// </summary>
        public void Pulse()
        {
            while (_isRunning)
            {
                lock (_syncObjectPulse)
                {
                    if (_tasks.Count != 0)
                    {
                        var task = _tasks.Dequeue();

                        task.Execute();
                    }
                }
            }
        }

        /// <summary>
        /// Processes player updates (informing characters of all updates about other character actions).
        /// </summary>
        public void PulsePlayerUpdates()
        {
            while (_isRunning)
            {
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// Processes mobs updates (informing characters of all updates about mob actions).
        /// </summary>
        public void PulseMobUpdates()
        {
            while (_isRunning)
            {
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// Offers a new task to be processed
        /// </summary>
        /// <param name="task">The task instance</param>
        public void Offer(Task.Task task)
        {
            lock (_syncObjectOffer)
            {
                _tasks.Enqueue(task);
            }
        }
    }
}