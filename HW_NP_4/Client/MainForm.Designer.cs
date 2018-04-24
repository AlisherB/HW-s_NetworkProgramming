namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.msg_textBox = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msg_textBox
            // 
            this.msg_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msg_textBox.Location = new System.Drawing.Point(0, 0);
            this.msg_textBox.Multiline = true;
            this.msg_textBox.Name = "msg_textBox";
            this.msg_textBox.Size = new System.Drawing.Size(372, 170);
            this.msg_textBox.TabIndex = 0;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(137, 176);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(102, 40);
            this.send_btn.TabIndex = 1;
            this.send_btn.Text = "Отправить";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.Send_btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 223);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.msg_textBox);
            this.MaximumSize = new System.Drawing.Size(388, 262);
            this.MinimumSize = new System.Drawing.Size(388, 262);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msg_textBox;
        private System.Windows.Forms.Button send_btn;
    }
}

