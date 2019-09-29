using Gma.System.MouseKeyHook;
using pro.models;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pro
{
    public partial class Form1 : Form
    {

        private IKeyboardMouseEvents m_GlobalHook;
        private SendDataHelper _sendDataHelper;
        private Data _data;
        private CheckPixelsHelper _checkPixelsHelper;
        private static bool counter = true;
        private bool go = true;
        CancellationToken cancellationToken;
        CancellationTokenSource _tokenSource;
        Task t1;
        public Form1()
        {
            InitializeComponent();
            _sendDataHelper = new SendDataHelper();
            m_GlobalHook = Hook.GlobalEvents();
            _data = new Data();
        }

        private void CatchPokemon()
        {
            while (go)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    go = false;
                }
                counter = !counter;
                if (go)
                    go = _checkPixelsHelper.Pokemon(_sendDataHelper, counter, _data);
            }
        }

        private void GetExpirience()
        {
            while (go)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    go = false;
                }
                counter = !counter;
                if (go)
                    go = _checkPixelsHelper.Exp(_sendDataHelper, counter, _data);
            }
        }

        private void Btn_cursor_Click(object sender, EventArgs e)
        {
            m_GlobalHook.MouseDownExt += GetPixels;
        }

        private void GetPixels(object sender, EventArgs e)
        {
            Point cursor = new Point();

            SendDataHelper.GetCursorPos(ref cursor);

            var c = _sendDataHelper.GetColorAt(cursor);
            this.BackColor = c;
            inA.Text = c.A.ToString();
            inB.Text = c.B.ToString();
            inR.Text = c.R.ToString();
            inG.Text = c.G.ToString();
            inX.Text = cursor.X.ToString();
            inY.Text = cursor.Y.ToString();
            m_GlobalHook.MouseDownExt -= GetPixels;
        }

        private void Player_btn_Click(object sender, EventArgs e)
        {
            m_GlobalHook.MouseDownExt += GetPlayerCoords;
        }

        public void GetPlayerCoords(object sender, MouseEventExtArgs e)
        {
            Point cursor = new Point();

            SendDataHelper.GetCursorPos(ref cursor);

            var c = _sendDataHelper.GetColorAt(cursor);

            this.BackColor = c;
            PlayerA.Text = c.A.ToString();
            PlayerB.Text = c.B.ToString();
            PlayerR.Text = c.R.ToString();
            PlayerG.Text = c.G.ToString();
            PlayerX.Text = cursor.X.ToString();
            PlayerY.Text = cursor.Y.ToString();

            m_GlobalHook.MouseDownExt -= GetPlayerCoords;
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            SetData();
            JsonHelper.Export(_data);
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            var path = importFileDialog.ShowDialog();
            _data = JsonHelper.Import(importFileDialog.FileName);
            SetInputs();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = true;
            SetData();
            _checkPixelsHelper = new CheckPixelsHelper(_data);
            go = true;
            _tokenSource = new CancellationTokenSource();
            cancellationToken = _tokenSource.Token;
            t1 = Task.Run(CatchPokemon, cancellationToken);
        }

        private void ExpBtn_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = true;
            SetData();
            _checkPixelsHelper = new CheckPixelsHelper(_data);
            go = true;
            _tokenSource = new CancellationTokenSource();
            cancellationToken = _tokenSource.Token;
            t1 = Task.Run(GetExpirience, cancellationToken);
        }

        private Data SetData()
        {
            _data.InA = Int32.Parse(inA.Text);
            _data.InR = Int32.Parse(inR.Text);
            _data.InG = Int32.Parse(inG.Text);
            _data.InB = Int32.Parse(inB.Text);
            _data.InX = Int32.Parse(inX.Text);
            _data.InY = Int32.Parse(inY.Text);
            _data.EvA = Int32.Parse(EvA.Text);
            _data.EvR = Int32.Parse(EvR.Text);
            _data.EvG = Int32.Parse(EvG.Text);
            _data.EvB = Int32.Parse(EvB.Text);
            _data.EvX = Int32.Parse(EvX.Text);
            _data.EvY = Int32.Parse(EvY.Text);
            _data.ExpA = Int32.Parse(ExpA.Text);
            _data.ExpR = Int32.Parse(ExpR.Text);
            _data.ExpG = Int32.Parse(ExpG.Text);
            _data.ExpB = Int32.Parse(ExpB.Text);
            _data.ExpX = Int32.Parse(ExpX.Text);
            _data.ExpY = Int32.Parse(ExpY.Text);
            _data.PlayerA = Int32.Parse(PlayerA.Text);
            _data.PlayerR = Int32.Parse(PlayerR.Text);
            _data.PlayerG = Int32.Parse(PlayerG.Text);
            _data.PlayerB = Int32.Parse(PlayerB.Text);
            _data.PlayerX = Int32.Parse(PlayerX.Text);
            _data.PlayerY = Int32.Parse(PlayerY.Text);
            _data.Key1 = Key1.Text;
            _data.Key2 = Key2.Text;
            _data.Key3 = Key3.Text;
            _data.Key4 = Key4.Text;
            _data.PP1 = Int32.Parse(PP1.Text);
            _data.PP2 = Int32.Parse(PP2.Text);
            _data.PP3 = Int32.Parse(PP3.Text);
            _data.PP4 = Int32.Parse(PP4.Text);
            return _data;
        }

        private void SetInputs()
        {
            inA.Text = _data.InA.ToString();
            inR.Text = _data.InR.ToString();
            inG.Text = _data.InG.ToString();
            inB.Text = _data.InB.ToString();
            inX.Text = _data.InX.ToString();
            inY.Text = _data.InY.ToString();
            EvA.Text = _data.EvA.ToString();
            EvR.Text = _data.EvR.ToString();
            EvG.Text = _data.EvG.ToString();
            EvB.Text = _data.EvB.ToString();
            EvX.Text = _data.EvX.ToString();
            EvY.Text = _data.EvY.ToString();
            ExpA.Text = _data.ExpA.ToString();
            ExpR.Text = _data.ExpR.ToString();
            ExpG.Text = _data.ExpG.ToString();
            ExpB.Text = _data.ExpB.ToString();
            ExpX.Text = _data.ExpX.ToString();
            ExpY.Text = _data.ExpY.ToString();
            PlayerA.Text = _data.PlayerA.ToString();
            PlayerR.Text = _data.PlayerR.ToString();
            PlayerG.Text = _data.PlayerG.ToString();
            PlayerB.Text = _data.PlayerB.ToString();
            PlayerX.Text = _data.PlayerX.ToString();
            PlayerY.Text = _data.PlayerY.ToString();
            Key1.Text = _data.Key1;
            Key2.Text = _data.Key2;
            Key3.Text = _data.Key3;
            Key4.Text = _data.Key4;
            PP1.Text = _data.PP1.ToString();
            PP2.Text = _data.PP2.ToString();
            PP3.Text = _data.PP3.ToString();
            PP4.Text = _data.PP4.ToString();
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = false;
            go = false;
            _tokenSource.Cancel();
            t1.Wait();
            t1.Dispose();
            _tokenSource.Dispose();
        }
    }
}
