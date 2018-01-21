﻿using CommeillFaut;
using CommeillFaut.DTOs;
using CommeillFautWF.Properties;
using GAIPS.AssetEditorTools;
using System;
using System.Windows.Forms;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
        private SocialExchangeDTO dto;
        private CommeillFautAsset asset;
        public Guid UpdatedGuid { get; private set; }

        public AddSocialExchange(CommeillFautAsset asset, SocialExchangeDTO dto)
        {
            InitializeComponent();

            this.dto = dto;
            this.asset = asset;

            //Validators 
            EditorTools.AllowOnlyGroundedLiteral(nameTextBox);
            EditorTools.AllowOnlyVariable(wfNameInitiator);
            EditorTools.AllowOnlyVariable(wfNameTarget);

            nameTextBox.Value = dto.Name;
            textBoxDescription.Text = dto.Description;
            wfNameInitiator.Value = dto.Initiator;
            wfNameTarget.Value = dto.Target;

            buttonAdd.Text = (dto.Id == Guid.Empty) ? "Add" : "Update";
        }


        private void AddSocialExchange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dto.Name = nameTextBox.Value;
                dto.Description = textBoxDescription.Text;
                dto.Target = wfNameTarget.Value;
                dto.Initiator = wfNameInitiator.Value;
                UpdatedGuid = asset.AddOrUpdateExchange(dto);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}