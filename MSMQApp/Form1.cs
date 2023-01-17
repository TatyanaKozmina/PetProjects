using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Messaging;
using System.Windows.Forms;

namespace MSMQApp
{
    public partial class frmMSMQ : Form
    {
        const string queueName = ".\\Private$\\TestQueue1";

        public frmMSMQ()
        {
            InitializeComponent();
        }

        private void btnCreateAndFill_Click(object sender, EventArgs e)
        {
            int numMessages = (int)this.numMessages.Value;

            if (!MessageQueue.Exists(queueName))
                MessageQueue.Create(queueName);

            MessageQueue msgQ = new MessageQueue(queueName);

            for (int i = 1; i <= numMessages; i++)
            {
                System.Messaging.Message msg = new System.Messaging.Message(new FundsHoldRS(i));
                msgQ.Send(msg);
            }

            MessageBox.Show($"В очередь {queueName} добавлено {numMessages} сообщений");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!MessageQueue.Exists(queueName))
                return;
            
            MessageQueue msgQ = new MessageQueue(queueName);
            msgQ.Purge();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {            
            if (!MessageQueue.Exists(queueName))
                return;
            
            MessageQueue msgQ = new MessageQueue(queueName);
            MessageBox.Show($"В очереди {msgQ.GetAllMessages().Length} сообщений"); 
        }        

        private void ProcessQueue(bool oldCode, int limit = int.MaxValue)
        {
            if (!MessageQueue.Exists(queueName))
                return;
            MessageQueue msgQ = new MessageQueue(queueName);
            var mesIds = new List<string>();

            if (oldCode)
            {
                var messages = msgQ.GetAllMessages();
                foreach(var msg in messages)
                {
                    // Здесь сообщения шифруются и подписываются
                    mesIds.Add(msg.Id);
                }

                var filter = new MessagePropertyFilter();
                filter.ClearAll();
                filter.Id = true;
                msgQ.MessageReadPropertyFilter = filter;

                var enumerator = msgQ.GetMessageEnumerator2();

                while (enumerator.MoveNext())
                {
                    foreach (var messageId in mesIds)
                    {
                        if (enumerator.Current.Id == messageId)
                            enumerator.RemoveCurrent();
                    }
                }
            }
            else
            {
                var enumerator = msgQ.GetMessageEnumerator2();
                int i = 0;
                while (enumerator.MoveNext())
                {
                    if (++i > limit)
                        break;

                    // Здесь сообщения шифруются и подписываются
                    mesIds.Add(enumerator.Current.Id);
                }

                var filter = new MessagePropertyFilter();
                filter.ClearAll();
                filter.Id = true;
                msgQ.MessageReadPropertyFilter = filter;

                enumerator.Reset();

                while (enumerator.MoveNext())
                {
                    foreach (var messageId in mesIds)
                    {
                        if (enumerator.Current.Id == messageId)
                            enumerator.RemoveCurrent();
                    }
                }
            } 
            //MessageBox.Show($"Обработка очереди завершена");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int limit = 500;
            //int numMessages = (int)this.numMessages.Value;
            int numMessages = 1000000;
            int loops = numMessages / limit;

            var timer = new Stopwatch();
            timer.Start();
            bool oldcode = this.chkOld.Checked;

            if (oldcode)
                ProcessQueue(oldcode);
            else
            {
                for (int i = 0; i < loops; i++)
                    ProcessQueue(oldcode);
            }            

            timer.Stop();

            MessageBox.Show($"Время первого прохода {timer.ElapsedMilliseconds}");
        }
    }

    public class FundsHoldRS
    {
        public FundsHoldRS() { }

        public FundsHoldRS(int pos)
        {
            position = pos;
            id = Guid.NewGuid();
        }
        public Guid id;
        public int position;
    }
}
