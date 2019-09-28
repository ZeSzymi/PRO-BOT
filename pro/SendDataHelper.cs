using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pro
{
    public class SendDataHelper
    {
        private Process _process;

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        static Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

        public SendDataHelper()
        {
            _process = Process.GetProcessesByName("PROclient").FirstOrDefault();
        }

        public Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public void SendKeyPressToProcess(int key)
        {
            if (_process != null)
            {
                IntPtr h = _process.MainWindowHandle;
                SetForegroundWindow(h);
                PostMessage(_process.MainWindowHandle, 0x0100, key, 0);
                Thread.Sleep(1000);
                SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            }
        }

        public void SendKeyToQueue(string key)
        {
            if (_process != null)
            {
                IntPtr h = _process.MainWindowHandle;
                SetForegroundWindow(h);
                SendKeys.SendWait(key);
                Thread.Sleep(1000);
                SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            }
        }

        public bool checkPixels(int a, int r, int g, int b, int x, int y)
        {
            Point cursor = new Point();
            cursor.X = x;
            cursor.Y = y;

            var c = GetColorAt(cursor);

            if (c.A == a && c.R == r && c.G == g && c.B == b)
            {
                return true;
            }
            return false;
        }

    }
}
