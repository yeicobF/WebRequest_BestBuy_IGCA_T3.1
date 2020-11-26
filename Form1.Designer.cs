namespace _T3._1__WebRequest_con_BestBuy
{
    partial class SearchWindow
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_WebQuery = new System.Windows.Forms.TextBox();
            this.label_InformationOfProject = new System.Windows.Forms.Label();
            this.label_WebQuery = new System.Windows.Forms.Label();
            this.label_WebQueryCategory = new System.Windows.Forms.Label();
            this.comboBox_SortBy = new System.Windows.Forms.ComboBox();
            this.button_WebSearch = new System.Windows.Forms.Button();
            this.listBox_Elements = new System.Windows.Forms.ListBox();
            this.listBox_ElementInformation = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBox_WebQuery
            // 
            this.textBox_WebQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_WebQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_WebQuery.Location = new System.Drawing.Point(93, 18);
            this.textBox_WebQuery.Name = "textBox_WebQuery";
            this.textBox_WebQuery.Size = new System.Drawing.Size(250, 23);
            this.textBox_WebQuery.TabIndex = 0;
            this.textBox_WebQuery.Tag = "Query";
            // 
            // label_InformationOfProject
            // 
            this.label_InformationOfProject.AutoSize = true;
            this.label_InformationOfProject.BackColor = System.Drawing.Color.Black;
            this.label_InformationOfProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_InformationOfProject.CausesValidation = false;
            this.label_InformationOfProject.Cursor = System.Windows.Forms.Cursors.No;
            this.label_InformationOfProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_InformationOfProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_InformationOfProject.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_InformationOfProject.Location = new System.Drawing.Point(12, 460);
            this.label_InformationOfProject.Name = "label_InformationOfProject";
            this.label_InformationOfProject.Size = new System.Drawing.Size(390, 92);
            this.label_InformationOfProject.TabIndex = 100;
            this.label_InformationOfProject.Tag = "Information";
            this.label_InformationOfProject.Text = "WebRequest con el sitio web de Best Buy\r\nINTERFACES GRÁFICAS CON APLICACIONES [3e" +
    "r PARCIAL]\r\nViernes, 27 de noviembre del 2020\r\n\r\nFlores Rodríguez Francisco Jaco" +
    "b\r\nGonzález Rodríguez Germán Antonio";
            this.label_InformationOfProject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_WebQuery
            // 
            this.label_WebQuery.AutoSize = true;
            this.label_WebQuery.BackColor = System.Drawing.Color.DarkRed;
            this.label_WebQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_WebQuery.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_WebQuery.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_WebQuery.Location = new System.Drawing.Point(12, 9);
            this.label_WebQuery.Name = "label_WebQuery";
            this.label_WebQuery.Size = new System.Drawing.Size(75, 40);
            this.label_WebQuery.TabIndex = 101;
            this.label_WebQuery.Text = "Elemento\r\na buscar:";
            this.label_WebQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_WebQueryCategory
            // 
            this.label_WebQueryCategory.AutoSize = true;
            this.label_WebQueryCategory.BackColor = System.Drawing.Color.DarkRed;
            this.label_WebQueryCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_WebQueryCategory.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_WebQueryCategory.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_WebQueryCategory.Location = new System.Drawing.Point(349, 18);
            this.label_WebQueryCategory.Name = "label_WebQueryCategory";
            this.label_WebQueryCategory.Size = new System.Drawing.Size(101, 21);
            this.label_WebQueryCategory.TabIndex = 102;
            this.label_WebQueryCategory.Text = "Ordenar por:";
            this.label_WebQueryCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_SortBy
            // 
            this.comboBox_SortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SortBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SortBy.Items.AddRange(new object[] {
            "Mejor Coincidencia",
            "Precio de Bajo a Alto",
            "Precio de Alto a Bajo",
            "Marcas A-Z",
            "Marcas Z-A",
            "Fecha de lanzamiento",
            "Recién Llegados",
            "Título A-Z",
            "Título Z-A"});
            this.comboBox_SortBy.Location = new System.Drawing.Point(456, 18);
            this.comboBox_SortBy.Name = "comboBox_SortBy";
            this.comboBox_SortBy.Size = new System.Drawing.Size(250, 24);
            this.comboBox_SortBy.TabIndex = 1;
            this.comboBox_SortBy.Tag = "Query";
            // 
            // button_WebSearch
            // 
            this.button_WebSearch.AutoSize = true;
            this.button_WebSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_WebSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_WebSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_WebSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_WebSearch.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WebSearch.Location = new System.Drawing.Point(722, 15);
            this.button_WebSearch.Margin = new System.Windows.Forms.Padding(0);
            this.button_WebSearch.Name = "button_WebSearch";
            this.button_WebSearch.Size = new System.Drawing.Size(64, 29);
            this.button_WebSearch.TabIndex = 2;
            this.button_WebSearch.Tag = "Query";
            this.button_WebSearch.Text = "Buscar";
            this.button_WebSearch.UseVisualStyleBackColor = false;
            // 
            // listBox_Elements
            // 
            this.listBox_Elements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Elements.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBox_Elements.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Elements.ItemHeight = 16;
            this.listBox_Elements.Location = new System.Drawing.Point(13, 78);
            this.listBox_Elements.Name = "listBox_Elements";
            this.listBox_Elements.Size = new System.Drawing.Size(389, 370);
            this.listBox_Elements.TabIndex = 3;
            this.listBox_Elements.Tag = "Information";
            // 
            // listBox_ElementInformation
            // 
            this.listBox_ElementInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_ElementInformation.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox_ElementInformation.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_ElementInformation.ItemHeight = 16;
            this.listBox_ElementInformation.Location = new System.Drawing.Point(456, 78);
            this.listBox_ElementInformation.Name = "listBox_ElementInformation";
            this.listBox_ElementInformation.Size = new System.Drawing.Size(330, 466);
            this.listBox_ElementInformation.TabIndex = 4;
            this.listBox_ElementInformation.Tag = "Information";
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.listBox_ElementInformation);
            this.Controls.Add(this.listBox_Elements);
            this.Controls.Add(this.button_WebSearch);
            this.Controls.Add(this.comboBox_SortBy);
            this.Controls.Add(this.label_WebQueryCategory);
            this.Controls.Add(this.label_WebQuery);
            this.Controls.Add(this.label_InformationOfProject);
            this.Controls.Add(this.textBox_WebQuery);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.Name = "SearchWindow";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Search From Best Buy Website";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_WebQuery;
        private System.Windows.Forms.Label label_InformationOfProject;
        private System.Windows.Forms.Label label_WebQuery;
        private System.Windows.Forms.Label label_WebQueryCategory;
        private System.Windows.Forms.ComboBox comboBox_SortBy;
        private System.Windows.Forms.Button button_WebSearch;
        private System.Windows.Forms.ListBox listBox_Elements;
        private System.Windows.Forms.ListBox listBox_ElementInformation;
    }
}

