using CardReaderApp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardReaderLibrary
{
    public class StudentIdCapture : Publisher<string>, IPublisher<string>, ISubscriber<Keys>
    {
        char startChar = ';';
        char endChar = '?';
        string bufferString = null;

        public void Play(Keys key)
        {
            Console.WriteLine(string.Format("Play key called: {0}", key));
            var keyChar = KeysConverter.ConvertFromKeys(key); 

            if (bufferString == null)
            {
                if (keyChar.HasValue && keyChar.Value == startChar)
                {
                    bufferString = string.Empty;
                }
            }
            else
            {
                if (keyChar.HasValue && keyChar.Value == endChar)
                {
                    if (bufferString != null && bufferString.Length == 14)
                    {
                        bufferString = bufferString.Substring(3, 7);

                        foreach (var subscriber in this.Subscribers)
                        {
                            subscriber.Play(bufferString);
                        }                        
                    }

                    bufferString = null;
                }
                else
                {
                    if (keyChar.HasValue)
                    {                        
                        bufferString += keyChar.Value;
                        Console.WriteLine(string.Format("Character added: {0}", keyChar.Value));
                        Console.WriteLine(string.Format("Built string: {0}", bufferString));
                    }
                }
            }
        }
    }
}
