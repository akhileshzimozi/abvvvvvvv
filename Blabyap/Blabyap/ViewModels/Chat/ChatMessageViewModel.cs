using System;
using System.Collections.Generic;
using System.Text;

namespace Blabyap.ViewModels.Chat
{
  public class ChatMessageViewModel :BaseViewModel
    {
        private string _chatMsg;

        public string ChatMsg
        {
            get { return _chatMsg; }
            set
            {
                _chatMsg = value;
                OnPropertyChanged("ChatMsg");

            }
        }
        private string _candidateEmail;
        public string CandidateEmail
        {
            get { return _candidateEmail; }
            set
            {
                _chatMsg = value;
                OnPropertyChanged("CandidateEmail");

            }
        }
        private bool _isMine;

        public bool IsMine
        {
            get { return _isMine; }
            set
            {
                _isMine = value;
                OnPropertyChanged("IsMine");
            }
        }

    }
}
