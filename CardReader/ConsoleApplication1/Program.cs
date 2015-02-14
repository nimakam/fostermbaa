using CardReaderApp;
using CardReaderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //GlobalKeyboardCapture.Instance.Subscribe(new ActionSubscriber<Keys>(delegate(Keys keys)
            //{
            //    Console.WriteLine(keys);
            //}));

            var studentIdCapture = new StudentIdCapture();

            GlobalKeyboardCapture.Instance.Subscribe(studentIdCapture);
            studentIdCapture.Subscribe(new ActionSubscriber<string>(delegate(string studentId)
            {
                Console.WriteLine("STUDENT ID: {0}", studentId);
            }));


            GlobalKeyboardCapture.Main();
        }
    }
}
