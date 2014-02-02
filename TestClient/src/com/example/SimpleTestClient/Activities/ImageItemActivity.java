package com.example.SimpleTestClient.Activities;

import android.app.Activity;
import android.graphics.Matrix;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import com.example.SimpleTestClient.Adapters.ImageAdapterItem;
import com.example.SimpleTestClient.R;
import uk.co.senab.photoview.PhotoViewAttacher;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 07.11.13
 * Time: 12:40
 * To change this template use File | Settings | File Templates.
 */
public class ImageItemActivity  extends Activity {

    static final String PHOTO_TAP_TOAST_STRING = "Photo Tap! X: %.2f %% Y:%.2f %%";
    static final String SCALE_TOAST_STRING = "Scaled to: %.2ff";

    public static ImageAdapterItem ShowingImage = null;

    private TextView mCurrMatrixTv;

    private PhotoViewAttacher mAttacher;

    private Toast mCurrentToast;

    private Matrix mCurrentDisplayMatrix = null;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_image_item);

        try{
          ImageView mImageView = (ImageView) findViewById(R.id.activity_image_item_iv_photo);
          mCurrMatrixTv = (TextView) findViewById(R.id.activity_image_item_tv_current_matrix);

          mImageView.setImageBitmap(ImageItemActivity.ShowingImage.bitmap);
          mCurrMatrixTv.setText(ImageItemActivity.ShowingImage.text);
            // The MAGIC happens here!
            mAttacher = new PhotoViewAttacher(mImageView);

            // Lets attach some listeners, not required though!
            // mAttacher.setOnMatrixChangeListener(new MatrixChangeListener());
            mAttacher.setOnPhotoTapListener(new PhotoTapListener());
        }
        catch (Exception ex)
        {
           finishActivity(0);
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();

        // Need to call clean-up
        mAttacher.cleanup();
    }



    private class PhotoTapListener implements PhotoViewAttacher.OnPhotoTapListener {

        @Override
        public void onPhotoTap(View view, float x, float y) {
            float xPercentage = x * 100f;
            float yPercentage = y * 100f;
        }
    }



}
