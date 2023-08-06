package com.damytech.YiTong;

import android.app.Activity;
import android.app.ProgressDialog;
import android.graphics.Bitmap;
import android.graphics.Rect;
import android.os.Bundle;
import android.view.View;
import android.view.ViewTreeObserver;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ProgressBar;
import android.widget.RelativeLayout;
import com.baidu.mobstat.StatService;
import com.damytech.CommService.CommMgr;
import com.damytech.HttpConn.JsonHttpResponseHandler;
import com.damytech.utils.ResolutionSet;
import org.json.JSONObject;

public class MainActivity extends Activity {
    RelativeLayout mainLayout;
    boolean bInitialized = false;

    private String strSiteAddr = "";
    private JsonHttpResponseHandler handlerSiteAddr;
    private ProgressDialog progDialog = null;

    private WebView webSite = null;
    private ProgressBar prgsStatus = null;
    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        StatService.setOn(this, StatService.EXCEPTION_LOG);
        StatService.setDebugOn(true);

        mainLayout = (RelativeLayout)findViewById(R.id.rlMainBack);
        mainLayout.getViewTreeObserver().addOnGlobalLayoutListener(
                new ViewTreeObserver.OnGlobalLayoutListener() {
                    public void onGlobalLayout() {
                        if (bInitialized == false)
                        {
                            Rect r = new Rect();
                            mainLayout.getLocalVisibleRect(r);
                            ResolutionSet._instance.setResolution(r.width(), r.height());
                            ResolutionSet._instance.iterateChild(findViewById(R.id.rlMainBack));
                            bInitialized = true;
                        }
                    }
                }
        );

        initControl();
        initHandler();

        CommMgr.commService.GetSiteAddr(handlerSiteAddr);
        progDialog = ProgressDialog.show(
                MainActivity.this,
                "",
                getString(R.string.waiting),
                true,
                false,
                null);
    }

    private void initControl()
    {
        prgsStatus = (ProgressBar) findViewById(R.id.prgsMain_Status);

        webSite = (WebView) findViewById(R.id.webMain_Site);
        webSite.getSettings().setJavaScriptEnabled(true);
        webSite.getSettings().setSupportZoom(true);
        webSite.setWebViewClient(new WebViewClient() {
            @Override
            public void onPageStarted(WebView view, String url, Bitmap favicon) {
                super.onPageStarted(view, url, favicon);
            }

            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                view.loadUrl(url);

                return true;
            }

            @Override
            public void onPageFinished(WebView view, String url) {
                super.onPageFinished(view, url);

                prgsStatus.setVisibility(View.GONE);
            }
        });

        return;
    }

    private void initHandler()
    {
        handlerSiteAddr = new JsonHttpResponseHandler()
        {
            int result = 0;

            @Override
            public void onSuccess(JSONObject jsonData)
            {
                progDialog.dismiss();

                result = 1;
                strSiteAddr = CommMgr.commService.parseGetSiteAddr(jsonData);
                if (webSite != null)
                {
                    webSite.loadUrl(strSiteAddr);
                }
            }

            @Override
            public void onFailure(Throwable ex, String exception)
            {
                progDialog.dismiss();
            }

            @Override
            public void onFinish()
            {
                result = 0;
            }
        };

        return;
    }

    @Override
    public void onResume()
    {
        super.onResume();

        StatService.onResume(this);
    }

    @Override
    public void onPause()
    {
        super.onPause();

        StatService.onPause(this);
    }
}
