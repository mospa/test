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
    public partial class 简易按键精灵 : Form
    {
        public 简易按键精灵()
        {
            InitializeComponent();
           // RegisterHotKey(Handle, 100, KeyModifiers.Alt, Keys.A);
          //  RegisterHotKey(Handle, 101, KeyModifiers.Alt, Keys.S);
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

        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(checkBox1.Checked){
            timer1.Interval = Convert.ToInt32(textBox7.Text.ToString());
           // keybd_event(0x09, 0x1e, 0x0001 | 0, 0);            //按下            
           // keybd_event(0x09, 0x1e, 0x0001 | 0x0002, 0); 
            //mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);
            /* mouse_event(0x0008 | 0x0010, 0, 0, 0, 0);
             keybd_event(0x31, 0x1e, 0x0001 | 0, 0);            //按下            
             keybd_event(0x31, 0x1e, 0x0001 | 0x0002, 0);        //弹起
             keybd_event(0x32, 0x1e, 0x0001 | 0, 0);            //按下            
             keybd_event(0x32, 0x1e, 0x0001 | 0x0002, 0);        //弹起
             keybd_event(0x33, 0x1e, 0x0001 | 0, 0);            //按下            
             keybd_event(0x33, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            
            keybd_event(0x34, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x34, 0x1e, 0x0001 | 0x0002, 0);
             * */
            keybd_event((byte)VkKeyScan(textBox3.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event((byte)VkKeyScan(textBox3.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0x0002, 0);        //弹起//弹起
            /* keybd_event(0x35, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x35, 0x1e, 0x0001 | 0x0002, 0);        //弹起
            keybd_event(0x36, 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event(0x36, 0x1e, 0x0001 | 0x0002, 0);        //弹起
           */ 
                }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (checkBox2.Checked) {
            timer2.Interval = Convert.ToInt32(textBox8.Text.ToString());
            keybd_event((byte)VkKeyScan(textBox4.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event((byte)VkKeyScan(textBox4.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0x0002, 0);        //弹起//弹起
            /* keybd_event(0x38, 0x1e, 0x0001 | 0, 0);            //按下            
             keybd_event(0x38, 0x1e, 0x0001 | 0x0002, 0);        //弹起
             */
            }
        }


        private void Form1_Leave(object sender, EventArgs e)
        {
            if (Handle.ToString() != null) { 
            //注销Id号为100的热键设定
            UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            UnregisterHotKey(Handle, 101);
            //注销Id号为102的热键设定
           // HotKey.UnregisterHotKey(Handle, 102);

            }
        }

        int count = 0;
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
                            if (count % 2 == 0) { 
                            //此处填写快捷键响应代码  
                            this.timer1.Start();
                            this.timer2.Start();
                            this.timer3.Start();
                            this.timer4.Start();
                            this.timer5.Start();
                            this.timer6.Start();
                            count++;
                            break;
                            }
                            else {
                                this.timer1.Stop();
                                this.timer2.Stop();
                                this.timer3.Stop();
                                this.timer4.Stop();
                                this.timer5.Stop();
                                this.timer6.Stop();
                                count++;
                                break;
                            }
                            
                            
                       /* case 101:    
                            //此处填写快捷键响应代码
                            this.timer1.Stop();
                            this.timer2.Stop();
                            this.timer3.Stop();
                            this.timer4.Stop();
                            this.timer5.Stop();
                            this.timer6.Stop();
                            break;  
                        * */
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
            // RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Shift, Keys.S);
            //注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。
            // RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.B);
            //注册热键Alt+D，Id号为102。HotKey.KeyModifiers.Alt也可以直接使用数字1来表示。
            //注销Id号为100的热键设定
            if (Handle.ToString() != null) { 
            UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            //UnregisterHotKey(Handle, 101);
            }
            //注销Id号为102的热键设定
            // HotKey.UnregisterHotKey(Handle, 102);
            RegisterHotKey(Handle, 100, KeyModifiers.None, (Keys)VkKeyScan(textBox1.Text.ToCharArray()[0]));
           // RegisterHotKey(Handle, 101, KeyModifiers.None, (Keys)VkKeyScan(textBox2.Text.ToCharArray()[0]));
           // RegisterHotKey(Handle, 100, KeyModifiers.Alt, Keys.S);
           // RegisterHotKey(Handle, 101, KeyModifiers.Alt, Keys.A);
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false; 
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox14.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (checkBox4.Checked) { 
            timer3.Interval = Convert.ToInt32(textBox9.Text.ToString()); 
            keybd_event((byte)VkKeyScan(textBox5.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event((byte)VkKeyScan(textBox5.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0x0002, 0);        //弹起//弹起
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (checkBox3.Checked) { 
            timer4.Interval = Convert.ToInt32(textBox11.Text.ToString()); 
            keybd_event((byte)VkKeyScan(textBox6.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0, 0);            //按下            
            keybd_event((byte)VkKeyScan(textBox6.Text.ToCharArray()[0]), 0x1e, 0x0001 | 0x0002, 0);        //弹起//弹起
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (checkBox6.Checked) { 
            timer1.Interval = Convert.ToInt32(textBox12.Text.ToString());
            mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            if (checkBox5.Checked) { 
            timer1.Interval = Convert.ToInt32(textBox14.Text.ToString());
            mouse_event(0x0008 | 0x0010, 0, 0, 0, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            textBox14.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
        }
    }
}
