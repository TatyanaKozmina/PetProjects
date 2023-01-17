namespace MSMQApp
{
    partial class frmMSMQ
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
            this.lblNumMessages = new System.Windows.Forms.Label();
            this.numMessages = new System.Windows.Forms.NumericUpDown();
            this.btnCreateAndFill = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCount = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkOld = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMessages)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumMessages
            // 
            this.lblNumMessages.AutoSize = true;
            this.lblNumMessages.Location = new System.Drawing.Point(10, 32);
            this.lblNumMessages.Name = "lblNumMessages";
            this.lblNumMessages.Size = new System.Drawing.Size(139, 16);
            this.lblNumMessages.TabIndex = 0;
            this.lblNumMessages.Text = "Добавить в очередь";
            // 
            // numMessages
            // 
            this.numMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numMessages.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMessages.Location = new System.Drawing.Point(171, 32);
            this.numMessages.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMessages.Name = "numMessages";
            this.numMessages.Size = new System.Drawing.Size(144, 18);
            this.numMessages.TabIndex = 1;
            this.numMessages.Value = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            // 
            // btnCreateAndFill
            // 
            this.btnCreateAndFill.Location = new System.Drawing.Point(334, 32);
            this.btnCreateAndFill.Name = "btnCreateAndFill";
            this.btnCreateAndFill.Size = new System.Drawing.Size(264, 35);
            this.btnCreateAndFill.TabIndex = 2;
            this.btnCreateAndFill.Text = "Создать и заполнить очередь";
            this.btnCreateAndFill.UseVisualStyleBackColor = true;
            this.btnCreateAndFill.Click += new System.EventHandler(this.btnCreateAndFill_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(31, 96);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(264, 35);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Очистить очередь";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCount
            // 
            this.btnCount.Location = new System.Drawing.Point(31, 39);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(264, 35);
            this.btnCount.TabIndex = 7;
            this.btnCount.Text = "Число сообщений в очереди";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(264, 35);
            this.button1.TabIndex = 8;
            this.button1.Text = "Замерить время";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCount);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Location = new System.Drawing.Point(331, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 161);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Служебные команды";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkOld);
            this.groupBox2.Controls.Add(this.btnCreateAndFill);
            this.groupBox2.Controls.Add(this.lblNumMessages);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.numMessages);
            this.groupBox2.Location = new System.Drawing.Point(32, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 142);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Диагностика";
            // 
            // chkOld
            // 
            this.chkOld.AutoSize = true;
            this.chkOld.Location = new System.Drawing.Point(13, 101);
            this.chkOld.Name = "chkOld";
            this.chkOld.Size = new System.Drawing.Size(104, 20);
            this.chkOld.TabIndex = 9;
            this.chkOld.Text = "Старый код";
            this.chkOld.UseVisualStyleBackColor = true;
            // 
            // frmMSMQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 383);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMSMQ";
            this.Text = "Форма для работы с очередями";
            ((System.ComponentModel.ISupportInitialize)(this.numMessages)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNumMessages;
        private System.Windows.Forms.NumericUpDown numMessages;
        private System.Windows.Forms.Button btnCreateAndFill;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkOld;
    }
}

