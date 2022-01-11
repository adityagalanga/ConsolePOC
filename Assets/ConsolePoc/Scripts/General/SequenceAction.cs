using System;
using System.Collections.Generic;

namespace Nagih
{
    public class SequenceAction
    {
        private int _progress;
        private Action[] _sequenceList;
        private HashSet<Action> _skipList;
        private Action _onComplete;
        private Action _onEverySequence;

        public void StartSequence(Action[] sequenceList, Action onComplete, Action onEverySequence = null)
        {
            _sequenceList = sequenceList;
            _onComplete = onComplete;
            _onEverySequence = onEverySequence;
            _progress = 0;
            _skipList = new HashSet<Action>();

            NextSequence();
        }

        public void NextSequence()
        {
            Action currentSequence = null;

            while(currentSequence == null && _progress < _sequenceList.Length)
            {
                currentSequence = _sequenceList[_progress++];
                if (_skipList.Contains(currentSequence))
                {
                    currentSequence = null;
                }
            }

            if(currentSequence != null)
            {
                currentSequence();
                _onEverySequence?.Invoke();
            }
            else
            {
                _sequenceList = null;
                _skipList = null;
                Util.ExecuteCallback(ref _onComplete);
            }
        }
    }
}