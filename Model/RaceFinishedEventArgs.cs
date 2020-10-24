using System;
using System.Collections.Generic;

namespace Model
{
    public class RaceFinishedEventArgs : EventArgs
    {
        public Stack<IParticipant> FinishedParticipants { get; set; }

        public RaceFinishedEventArgs(Stack<IParticipant> finishedParticipants)
        {
            FinishedParticipants = finishedParticipants;
        }
    }
}
