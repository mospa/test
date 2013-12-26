using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; 
namespace wowajjl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
           // RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Shift, Keys.S);
            //注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。
           // RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.B);
            //注册热键Alt+D，Id号为102。HotKey.KeyModifiers.Alt也可以直接使用数字1来表示。
            RegisterHotKey(Handle, 100, KeyModifiers.Alt, Keys.A);
            RegisterHotKey(Handle, 101, KeyModifiers.Alt, Keys.S);
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）          
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
           // keybd_event(0x09, 0x1e, 0x0001 | 0, 0);            //按下            
           // keybd_event(0x09, 0x1e, 0x0001 | 0x0002, 0); 
            mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);
            keybd_event(0x31, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x31, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            keybd_event(0x32, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x32, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            keybd_event(0x33, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x33, 0x1e, 0x0001 | 0x0002, 0);        //弹起
          /*  keybd_event(0x34, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x34, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            keybd_event(0x35, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x35, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            keybd_event(0x36, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x36, 0x1e, 0x0001 | 0x0002, 0);        //弹起
           */ 
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           /* keybd_event(0x37, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x37, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            keybd_event(0x38, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x38, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.timer1.Start();
           // this.timer2.Start();
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.timer1.Stop();
           // this.timer2.Stop(); 
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            UnregisterHotKey(Handle, 101);
            //注销Id号为102的热键设定
           // HotKey.UnregisterHotKey(Handle, 102);
        }


        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    
                            //此处填写快捷键响应代码  
                            this.timer1.Start();
                            break;
                        case 101:    
                            //此处填写快捷键响应代码
                            this.timer1.Stop();
                            break;                       
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
