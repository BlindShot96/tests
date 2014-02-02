package com.example.SimpleTestClient.Activities;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;

/**
 * Created by пётр on 09.01.14.
 */
public class RegisterActivity extends Activity
{
    Button NextBtn;
    EditText NameBox;
    EditText LastNameBox;
    EditText GroupBox;

        private void Initialize()
        {
            NextBtn =(Button)findViewById(R.id.activity_register_next_btn);
            NextBtn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                   NextButtonClick();
                }
            });

            NameBox = (EditText)findViewById(R.id.activity_register_name_box);
            LastNameBox = (EditText)findViewById(R.id.activity_register_lastname_box);
            GroupBox =(EditText)findViewById(R.id.activity_register_group_box);

            NameBox.setText(Common.GlobalValues.ClientCommon.Data.Name);
            LastNameBox.setText(Common.GlobalValues.ClientCommon.Data.LastName);
            GroupBox.setText(Common.GlobalValues.ClientCommon.Data.Group);
        }

        @Override
        protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.register);
            Initialize();
        }


    private void NextButtonClick()
    {
        boolean can_show = false;

        if ((NameBox.getText().toString().hashCode() == 0)) {
            can_show = true;
        }
        if (LastNameBox.getText().toString().hashCode() == 0) {
            can_show = true;
        }

        if (GroupBox.getText().toString().hashCode() == 0) {
            can_show = true;
        }

        if (can_show == false) {
            Common.GlobalValues.ClientCommon.Data.Name = NameBox.getText().toString();
            Common.GlobalValues.ClientCommon.Data.LastName = LastNameBox.getText().toString();
            Common.GlobalValues.ClientCommon.Data.Group = GroupBox.getText().toString();

            startActivity(new Intent(RegisterActivity.this,TestDownloadActivty.class));
        }
        else
        {
          Common.CommonMethods.DialogUtils.ShowErrorMessage("Не все поля заполнены!",this);
        }
    }



}

