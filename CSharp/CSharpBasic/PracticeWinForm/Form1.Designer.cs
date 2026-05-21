
namespace PracticeWinForm
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBox = new System.Windows.Forms.GroupBox();
            this.percentLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.syncButton = new System.Windows.Forms.Button();
            this.homeworkBox = new System.Windows.Forms.GroupBox();
            this.taskIntButton = new System.Windows.Forms.Button();
            this.taskButton = new System.Windows.Forms.Button();
            this.voidButton = new System.Windows.Forms.Button();
            this.msgButton = new System.Windows.Forms.Button();
            this.progressBox.SuspendLayout();
            this.homeworkBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBox
            // 
            this.progressBox.Controls.Add(this.percentLabel);
            this.progressBox.Controls.Add(this.progressBar);
            this.progressBox.Controls.Add(this.syncButton);
            this.progressBox.Font = new System.Drawing.Font("굴림", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.progressBox.Location = new System.Drawing.Point(19, 42);
            this.progressBox.Margin = new System.Windows.Forms.Padding(5);
            this.progressBox.Name = "progressBox";
            this.progressBox.Padding = new System.Windows.Forms.Padding(5);
            this.progressBox.Size = new System.Drawing.Size(1199, 567);
            this.progressBox.TabIndex = 0;
            this.progressBox.TabStop = false;
            this.progressBox.Text = "진척율";
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Font = new System.Drawing.Font("궁서체", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.percentLabel.Location = new System.Drawing.Point(561, 469);
            this.percentLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(46, 24);
            this.percentLabel.TabIndex = 3;
            this.percentLabel.Text = "0 %";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(33, 369);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar.Name = "progressBar";
            this.progressBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.progressBar.Size = new System.Drawing.Size(1102, 61);
            this.progressBar.TabIndex = 2;
            // 
            // syncButton
            // 
            this.syncButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.syncButton.Font = new System.Drawing.Font("궁서체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.syncButton.Location = new System.Drawing.Point(33, 72);
            this.syncButton.Margin = new System.Windows.Forms.Padding(5);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(321, 210);
            this.syncButton.TabIndex = 1;
            this.syncButton.Text = "Synchronous";
            this.syncButton.UseVisualStyleBackColor = false;
            this.syncButton.Click += new System.EventHandler(this.SyncButtonClick);
            // 
            // homeworkBox
            // 
            this.homeworkBox.Controls.Add(this.taskIntButton);
            this.homeworkBox.Controls.Add(this.taskButton);
            this.homeworkBox.Controls.Add(this.voidButton);
            this.homeworkBox.Font = new System.Drawing.Font("굴림", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.homeworkBox.Location = new System.Drawing.Point(19, 912);
            this.homeworkBox.Margin = new System.Windows.Forms.Padding(5);
            this.homeworkBox.Name = "homeworkBox";
            this.homeworkBox.Padding = new System.Windows.Forms.Padding(5);
            this.homeworkBox.Size = new System.Drawing.Size(1199, 488);
            this.homeworkBox.TabIndex = 0;
            this.homeworkBox.TabStop = false;
            this.homeworkBox.Text = "과제";
            // 
            // taskIntButton
            // 
            this.taskIntButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.taskIntButton.Font = new System.Drawing.Font("궁서체", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.taskIntButton.Location = new System.Drawing.Point(787, 75);
            this.taskIntButton.Margin = new System.Windows.Forms.Padding(5);
            this.taskIntButton.Name = "taskIntButton";
            this.taskIntButton.Size = new System.Drawing.Size(321, 390);
            this.taskIntButton.TabIndex = 2;
            this.taskIntButton.Text = "반환형 Task<int>";
            this.taskIntButton.UseVisualStyleBackColor = false;
            this.taskIntButton.Click += new System.EventHandler(this.TaskIntButtonClick);
            // 
            // taskButton
            // 
            this.taskButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.taskButton.Font = new System.Drawing.Font("궁서체", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.taskButton.Location = new System.Drawing.Point(435, 75);
            this.taskButton.Margin = new System.Windows.Forms.Padding(5);
            this.taskButton.Name = "taskButton";
            this.taskButton.Size = new System.Drawing.Size(321, 390);
            this.taskButton.TabIndex = 1;
            this.taskButton.Text = "반환형 Task";
            this.taskButton.UseVisualStyleBackColor = false;
            this.taskButton.Click += new System.EventHandler(this.TaskButtonClick);
            // 
            // voidButton
            // 
            this.voidButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.voidButton.Font = new System.Drawing.Font("궁서", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.voidButton.Location = new System.Drawing.Point(91, 75);
            this.voidButton.Margin = new System.Windows.Forms.Padding(5);
            this.voidButton.Name = "voidButton";
            this.voidButton.Size = new System.Drawing.Size(321, 390);
            this.voidButton.TabIndex = 0;
            this.voidButton.Text = "반환형 void";
            this.voidButton.UseVisualStyleBackColor = false;
            this.voidButton.Click += new System.EventHandler(this.VoidButtonClick);
            // 
            // msgButton
            // 
            this.msgButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.msgButton.Font = new System.Drawing.Font("궁서체", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.msgButton.Location = new System.Drawing.Point(444, 648);
            this.msgButton.Margin = new System.Windows.Forms.Padding(5);
            this.msgButton.Name = "msgButton";
            this.msgButton.Size = new System.Drawing.Size(349, 178);
            this.msgButton.TabIndex = 3;
            this.msgButton.Text = "메시지 출력";
            this.msgButton.UseVisualStyleBackColor = false;
            this.msgButton.Click += new System.EventHandler(this.MsgButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 1421);
            this.Controls.Add(this.msgButton);
            this.Controls.Add(this.homeworkBox);
            this.Controls.Add(this.progressBox);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.Text = "비동기처리 실습";
            this.progressBox.ResumeLayout(false);
            this.progressBox.PerformLayout();
            this.homeworkBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox progressBox;
        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.GroupBox homeworkBox;
        private System.Windows.Forms.Button taskIntButton;
        private System.Windows.Forms.Button taskButton;
        private System.Windows.Forms.Button voidButton;
        private System.Windows.Forms.Button msgButton;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

