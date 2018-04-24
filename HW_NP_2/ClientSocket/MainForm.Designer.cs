namespace ClientSocket
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
            this.label1 = new System.Windows.Forms.Label();
            this.index_label = new System.Windows.Forms.Label();
            this.index_textBox = new System.Windows.Forms.TextBox();
            this.find_button = new System.Windows.Forms.Button();
            this.city_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Город:";
            // 
            // index_label
            // 
            this.index_label.AutoSize = true;
            this.index_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.index_label.Location = new System.Drawing.Point(12, 58);
            this.index_label.Name = "index_label";
            this.index_label.Size = new System.Drawing.Size(60, 17);
            this.index_label.TabIndex = 2;
            this.index_label.Text = "Индекс:";
            // 
            // index_textBox
            // 
            this.index_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.index_textBox.Location = new System.Drawing.Point(82, 57);
            this.index_textBox.Name = "index_textBox";
            this.index_textBox.ReadOnly = true;
            this.index_textBox.Size = new System.Drawing.Size(165, 26);
            this.index_textBox.TabIndex = 3;
            // 
            // find_button
            // 
            this.find_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.find_button.Location = new System.Drawing.Point(253, 57);
            this.find_button.Name = "find_button";
            this.find_button.Size = new System.Drawing.Size(67, 26);
            this.find_button.TabIndex = 4;
            this.find_button.Text = "Поиск";
            this.find_button.UseVisualStyleBackColor = true;
            this.find_button.Click += new System.EventHandler(this.Find_button_Click);
            // 
            // city_textBox
            // 
            this.city_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.city_textBox.Location = new System.Drawing.Point(82, 15);
            this.city_textBox.Name = "city_textBox";
            this.city_textBox.Size = new System.Drawing.Size(238, 26);
            this.city_textBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 102);
            this.Controls.Add(this.city_textBox);
            this.Controls.Add(this.find_button);
            this.Controls.Add(this.index_textBox);
            this.Controls.Add(this.index_label);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label index_label;
        private System.Windows.Forms.TextBox index_textBox;
        private System.Windows.Forms.Button find_button;
        private System.Windows.Forms.TextBox city_textBox;
    }
}

