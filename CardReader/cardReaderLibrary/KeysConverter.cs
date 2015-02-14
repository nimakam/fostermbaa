using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardReaderLibrary
{
    public static class KeysConverter
    {
        public static Keys? ConvertToKeys(char character)
        {
            switch(character)
            {
                case '0':
                    return Keys.D0;
                case '1':
                    return Keys.D1;
                case '2':
                    return Keys.D2;
                case '3':
                    return Keys.D3;
                case '4':
                    return Keys.D4;
                case '5':
                    return Keys.D5;
                case '6':
                    return Keys.D6;
                case '7':
                    return Keys.D7;
                case '8':
                    return Keys.D8;
                case '9':
                    return Keys.D9;
                case '?':
                    return Keys.OemQuestion;
                case ';':
                    return Keys.OemSemicolon;
                default:
                    return null;
            }
        }


        public static char? ConvertFromKeys(Keys keys)
        {
            switch (keys)
            {
                case Keys.D0:
                    return '0';
                case Keys.D1:
                    return '1';
                case Keys.D2:
                    return '2';
                case Keys.D3:
                    return '3';
                case Keys.D4:
                    return '4';
                case Keys.D5:
                    return '5';
                case Keys.D6:
                    return '6';
                case Keys.D7:
                    return '7';
                case Keys.D8:
                    return '8';
                case Keys.D9:
                    return '9';
                case Keys.OemQuestion:
                    return '?';
                case Keys.OemSemicolon:
                    return ';';
                default:
                    return null;
            }
        }
    }
}
