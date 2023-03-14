using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    //displays notification details
    public partial class F2_MainFormNotificationWindow : Form
    {
        //CONSTRUCTORS
        public F2_MainFormNotificationWindow(Notification notification)
        {
            InitializeComponent();
            string[] importance = { "Regular notification","Important notification" };

            //set form properties
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.Text = importance[notification.getImportance()];
            this.F2_richTextBox_notificationText.Text = notification.getText();
            this.F2_textBox_sender.Text = notification.getSender();
            this.F2_textBox_dateSent.Text = notification.getDate();

            if (notification.getImportance() == 1) //mark the notification as important
                this.F2_pictureBox_notificationImportance.Image = Bitmap.FromFile(Utility.IMG_notifications_icons[2]);
        }


        //EVENT HANDLERS
        private void F2_MainFormNotificationWindow_Load(object sender, EventArgs e)
        {
            //load language pack
            Utility.setLanguage(this);
        }
    }
}
