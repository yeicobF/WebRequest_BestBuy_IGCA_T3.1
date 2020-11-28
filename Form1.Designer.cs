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
            this.components = new System.ComponentModel.Container();
            this.textBox_WebQuery = new System.Windows.Forms.TextBox();
            this.label_InformationOfProject = new System.Windows.Forms.Label();
            this.label_WebQuery = new System.Windows.Forms.Label();
            this.label_WebQueryCategory = new System.Windows.Forms.Label();
            this.comboBox_SortBy = new System.Windows.Forms.ComboBox();
            this.button_WebSearch = new System.Windows.Forms.Button();
            this.listBox_Products = new System.Windows.Forms.ListBox();
            this.label_Products = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.label_NumberOfFoundElements = new System.Windows.Forms.Label();
            this.button_SaveProductDetailsInFile = new System.Windows.Forms.Button();
            this.richTextBox_ProductDetails = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.searchErrorProvider)).BeginInit();
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
            this.textBox_WebQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_WebQuery_KeyDown);
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
            this.label_InformationOfProject.Location = new System.Drawing.Point(36, 460);
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
            this.comboBox_SortBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_SortBy_KeyDown);
            // 
            // button_WebSearch
            // 
            this.button_WebSearch.AutoSize = true;
            this.button_WebSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_WebSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_WebSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_WebSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_WebSearch.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WebSearch.Location = new System.Drawing.Point(722, 16);
            this.button_WebSearch.Margin = new System.Windows.Forms.Padding(0);
            this.button_WebSearch.Name = "button_WebSearch";
            this.button_WebSearch.Size = new System.Drawing.Size(64, 29);
            this.button_WebSearch.TabIndex = 2;
            this.button_WebSearch.Tag = "Query";
            this.button_WebSearch.Text = "Buscar";
            this.button_WebSearch.UseVisualStyleBackColor = false;
            this.button_WebSearch.Click += new System.EventHandler(this.button_WebSearch_Click);
            // 
            // listBox_Products
            // 
            this.listBox_Products.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Products.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBox_Products.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Products.HorizontalScrollbar = true;
            this.listBox_Products.ItemHeight = 16;
            this.listBox_Products.Location = new System.Drawing.Point(13, 111);
            this.listBox_Products.Name = "listBox_Products";
            this.listBox_Products.Size = new System.Drawing.Size(437, 290);
            this.listBox_Products.TabIndex = 3;
            this.listBox_Products.Tag = "Information";
            this.listBox_Products.SelectedIndexChanged += new System.EventHandler(this.listBox_Products_SelectedIndexChanged);
            this.listBox_Products.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Products_KeyDown);
            // 
            // label_Products
            // 
            this.label_Products.BackColor = System.Drawing.Color.DarkRed;
            this.label_Products.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Products.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Products.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_Products.Location = new System.Drawing.Point(12, 62);
            this.label_Products.Name = "label_Products";
            this.label_Products.Size = new System.Drawing.Size(438, 46);
            this.label_Products.TabIndex = 103;
            this.label_Products.Text = "LISTA DE PRODUCTOS";
            this.label_Products.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(456, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 21);
            this.label1.TabIndex = 104;
            this.label1.Text = "INFORMACIÓN DEL PRODUCTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchErrorProvider
            // 
            this.searchErrorProvider.BlinkRate = 200;
            this.searchErrorProvider.ContainerControl = this;
            this.searchErrorProvider.Tag = "Errors";
            // 
            // button_Reset
            // 
            this.button_Reset.AutoSize = true;
            this.button_Reset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_Reset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Reset.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.button_Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson;
            this.button_Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Reset.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Reset.ForeColor = System.Drawing.Color.White;
            this.button_Reset.Location = new System.Drawing.Point(456, 521);
            this.button_Reset.Margin = new System.Windows.Forms.Padding(0);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(117, 31);
            this.button_Reset.TabIndex = 5;
            this.button_Reset.Tag = "";
            this.button_Reset.Text = "Reiniciar todo";
            this.button_Reset.UseVisualStyleBackColor = false;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.AutoSize = true;
            this.button_Exit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Exit.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.button_Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson;
            this.button_Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Exit.Font = new System.Drawing.Font("Malgun Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.ForeColor = System.Drawing.Color.White;
            this.button_Exit.Location = new System.Drawing.Point(735, 521);
            this.button_Exit.Margin = new System.Windows.Forms.Padding(0);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(51, 31);
            this.button_Exit.TabIndex = 7;
            this.button_Exit.Tag = "";
            this.button_Exit.Text = "Salir";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // label_NumberOfFoundElements
            // 
            this.label_NumberOfFoundElements.BackColor = System.Drawing.Color.DarkRed;
            this.label_NumberOfFoundElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_NumberOfFoundElements.Font = new System.Drawing.Font("Malgun Gothic", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NumberOfFoundElements.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_NumberOfFoundElements.Location = new System.Drawing.Point(13, 403);
            this.label_NumberOfFoundElements.Name = "label_NumberOfFoundElements";
            this.label_NumberOfFoundElements.Size = new System.Drawing.Size(438, 53);
            this.label_NumberOfFoundElements.TabIndex = 111;
            this.label_NumberOfFoundElements.Text = "NÚMERO TOTAL DE PRODUCTOS ENCONTRADOS: --\r\nNÚMERO TOTAL DE PÁGINAS DE LA BÚSQUEDA" +
    ": --\r\n-> NÚMERO DE PRODUCTO SELECCIONADO: --";
            this.label_NumberOfFoundElements.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_SaveProductDetailsInFile
            // 
            this.button_SaveProductDetailsInFile.AutoSize = true;
            this.button_SaveProductDetailsInFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_SaveProductDetailsInFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_SaveProductDetailsInFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_SaveProductDetailsInFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_SaveProductDetailsInFile.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.button_SaveProductDetailsInFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson;
            this.button_SaveProductDetailsInFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button_SaveProductDetailsInFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SaveProductDetailsInFile.Font = new System.Drawing.Font("Malgun Gothic", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SaveProductDetailsInFile.ForeColor = System.Drawing.Color.White;
            this.button_SaveProductDetailsInFile.Location = new System.Drawing.Point(581, 518);
            this.button_SaveProductDetailsInFile.Margin = new System.Windows.Forms.Padding(0);
            this.button_SaveProductDetailsInFile.Name = "button_SaveProductDetailsInFile";
            this.button_SaveProductDetailsInFile.Size = new System.Drawing.Size(146, 38);
            this.button_SaveProductDetailsInFile.TabIndex = 6;
            this.button_SaveProductDetailsInFile.Tag = "File";
            this.button_SaveProductDetailsInFile.Text = "Guardar información\r\ndel producto en archivo";
            this.button_SaveProductDetailsInFile.UseVisualStyleBackColor = false;
            this.button_SaveProductDetailsInFile.Click += new System.EventHandler(this.button_SaveProductDetailsInFile_Click);
            // 
            // richTextBox_ProductDetails
            // 
            this.richTextBox_ProductDetails.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox_ProductDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_ProductDetails.BulletIndent = 1;
            this.richTextBox_ProductDetails.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_ProductDetails.HideSelection = false;
            this.richTextBox_ProductDetails.Location = new System.Drawing.Point(456, 94);
            this.richTextBox_ProductDetails.Name = "richTextBox_ProductDetails";
            this.richTextBox_ProductDetails.ReadOnly = true;
            this.richTextBox_ProductDetails.Size = new System.Drawing.Size(330, 417);
            this.richTextBox_ProductDetails.TabIndex = 4;
            this.richTextBox_ProductDetails.Tag = "Information";

            /* Hacer que el texto se centre.**/
            this.richTextBox_ProductDetails.SelectAll();
            this.richTextBox_ProductDetails.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;

            this.richTextBox_ProductDetails.Text = "Descripción del producto seleccionado.";
            this.richTextBox_ProductDetails.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_ProductDetails_LinkClicked);
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.button_SaveProductDetailsInFile);
            this.Controls.Add(this.label_NumberOfFoundElements);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_Products);
            this.Controls.Add(this.listBox_Products);
            this.Controls.Add(this.button_WebSearch);
            this.Controls.Add(this.comboBox_SortBy);
            this.Controls.Add(this.label_WebQueryCategory);
            this.Controls.Add(this.label_WebQuery);
            this.Controls.Add(this.label_InformationOfProject);
            this.Controls.Add(this.textBox_WebQuery);
            this.Controls.Add(this.richTextBox_ProductDetails);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.Name = "SearchWindow";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Form";
            this.Text = "Search From Best Buy Website";
            ((System.ComponentModel.ISupportInitialize)(this.searchErrorProvider)).EndInit();
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
        private System.Windows.Forms.ListBox listBox_Products;
        private System.Windows.Forms.Label label_Products;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider searchErrorProvider;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Label label_NumberOfFoundElements;
        private System.Windows.Forms.Button button_SaveProductDetailsInFile;
        private System.Windows.Forms.RichTextBox richTextBox_ProductDetails;
    }
}

