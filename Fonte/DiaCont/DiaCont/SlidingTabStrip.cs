using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace DiaCont
{
    public class SlidingTabStrip : LinearLayout
    {
        private const int DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS = 0;
        private const byte DEFAULT_BOTTOM_BORDER_COLOR_ALPHA = 0x26;
        
        //Grossura do indicador
        private const int SELECTED_INDICATOR_THICKNESS_DIPS = 5;
        
        //Vetor de cores de indicadores
        private int[] INDICATOR_COLORS = { 0xffffff };
        private int[] DIVIDER_COLORS = { 0xC5C5C5 };

        private const int DEFAULT_DIVIDER_THICKNESS_DIPS = 1;
        private const float DEFAULT_DIVIDER_HEIGHT = 0.5f;

        //Borda superior
        private int mBottomBorderThickness;
        private Paint mBottomBorderPaint;
        private int mDefaultBottomBorderColor;

        //Indicador
        private int mSelectedIndicatorThickness;
        private Paint mSelectedIndicatorPaint;

        //Divisor
        private Paint mDividerPaint;
        private float mDividerHeight;

        //Seletor de posição
        private int mSelectedPosition;
        private float mSelectionOffset;

        //Cor das tabs
        private SlidingTabScrollView.TabColorizer mCustomTabColorizer;
        private SimpleTabColorizer mDefaultTabColorizer;

        //Construtor
        public SlidingTabStrip(Context context) : this(context, null) { }
       
        //Construtor
        public SlidingTabStrip(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            SetWillNotDraw(false);
            
            //Densidado da tela (ppi)
            float density = Resources.DisplayMetrics.Density;

            TypedValue outValue = new TypedValue();
            context.Theme.ResolveAttribute(Android.Resource.Attribute.ColorForeground, outValue, true);
            int themeForeGround = outValue.Data;
            mDefaultBottomBorderColor = SetColorAlpha(themeForeGround, DEFAULT_BOTTOM_BORDER_COLOR_ALPHA);

            //Cores do indicador e dos divisores das tabs
            mDefaultTabColorizer = new SimpleTabColorizer();
            mDefaultTabColorizer.IndicatorColors = INDICATOR_COLORS;
            mDefaultTabColorizer.DividerColors = DIVIDER_COLORS;

            //Personalização da borda superior
            mBottomBorderThickness = (int)(DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS * density);
            mBottomBorderPaint = new Paint();
            mBottomBorderPaint.Color = GetColorFromInteger(0x6495ed); //Seta a cor cinza
            
            //Personalização do indicador
            mSelectedIndicatorThickness = (int)(SELECTED_INDICATOR_THICKNESS_DIPS * density);
            mSelectedIndicatorPaint = new Paint();
            
            //Personalização do Divisor
            mDividerHeight = DEFAULT_DIVIDER_HEIGHT;
            mDividerPaint = new Paint();
            mDividerPaint.StrokeWidth = (int)(DEFAULT_DIVIDER_THICKNESS_DIPS * density);
        }

        //Seta os valores de cor
        public SlidingTabScrollView.TabColorizer CustomTabColorizer
        {
            set
            {
                mCustomTabColorizer = value;
                this.Invalidate();    
            }
        }
        
        public int[] SelectedIndicatorColors
        {
            set
            {
                mCustomTabColorizer = null;
                mDefaultTabColorizer.IndicatorColors = value;
                this.Invalidate();
            }
        }
       
        public int[] DividerColors
        {
            set
            {
                mDefaultTabColorizer = null;
                mDefaultTabColorizer.DividerColors = value;
                this.Invalidate();
            }
        }
        

        //Transforma um inteiro em uma cor
        private Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }

        private int SetColorAlpha(int color, byte alpha)
        {
            return Color.Argb(alpha, Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }

        //Percebe que a pagina mudou (Scrolled)
        public void OnViewPagerPageChanged(int position, float positionOffset)
        {
            mSelectedPosition = position;
            mSelectionOffset = positionOffset;
            this.Invalidate();
        }

        protected override void OnDraw(Canvas canvas)
        {
            int height = Height;
            int tabCount = ChildCount;
            
            //Divide height em pixels
            int dividerHeightPx = (int)(Math.Min(Math.Max(0f, mDividerHeight), 1f) * height);

            SlidingTabScrollView.TabColorizer tabColorizer = mCustomTabColorizer != null ? mCustomTabColorizer : mDefaultTabColorizer;

            if(tabCount > 0)
            {
                View selectedTitle = GetChildAt(mSelectedPosition);
                int left = selectedTitle.Left;
                int right = selectedTitle.Right;
                
                //Seleciona a cor da tab
                int color = tabColorizer.GetIndicatorColor(mSelectedPosition);

                //Seleciona a cor da proxima tabela
                if (mSelectionOffset > 0F && mSelectedPosition < (tabCount - 1))
                {
                    int nextColor = tabColorizer.GetIndicatorColor(mSelectedPosition + 1);
                    if (color != nextColor)
                    {
                        color = blendColor(nextColor, color, mSelectionOffset);
                    }

                    //Chama as views a direita e a esquerda da atual
                    View nextTitle = GetChildAt(mSelectedPosition + 1);
                    left = (int)(mSelectionOffset * nextTitle.Left + (1.0f - mSelectionOffset) * left);
                    right = (int)(mSelectionOffset * nextTitle.Right + (1.0f - mSelectionOffset) * right);
                }

                mSelectedIndicatorPaint.Color = GetColorFromInteger(color);
                canvas.DrawRect(left, height - mSelectedIndicatorThickness, right, height, mSelectedIndicatorPaint);

                //Cria os divisores
                int separatorTop = (height * dividerHeightPx) / 2;
                for(int i = 0; i<ChildCount ; i++)
                {
                    View child = GetChildAt(i);
                    mDividerPaint.Color = GetColorFromInteger(tabColorizer.GetDividerColor(i));
                    canvas.DrawLine(child.Right, separatorTop, child.Right, separatorTop + dividerHeightPx, mDividerPaint);
                }

                canvas.DrawRect(0, height - mBottomBorderThickness, Width, height, mBottomBorderPaint);
            }
        }

        private int blendColor(int color1, int color2, float ratio)
        {
            float inverseRatio = 1f - ratio;
            float r = (Color.GetRedComponent(color1) * ratio) + (Color.GetRedComponent(color2) * inverseRatio);
            float g = (Color.GetGreenComponent(color1) * ratio) + (Color.GetGreenComponent(color2) * inverseRatio);
            float b = (Color.GetBlueComponent(color1) * ratio) + (Color.GetBlueComponent(color2) * inverseRatio);

            return Color.Rgb((int)r, (int)g, (int)b);
        }

        private class SimpleTabColorizer : SlidingTabScrollView.TabColorizer
        {
            //Vetores de cores
            private int[] mIndicatorColors;
            private int[] mDividerColors;

            //Retorna a cor do indicador da posição da tab
            public int GetIndicatorColor(int position)
            {
                return mIndicatorColors[position % mIndicatorColors.Length];
            }
            //Retorna a cor do divisor da posição da tab
            public int GetDividerColor(int position)
            {
                return mDividerColors[position % mDividerColors.Length];
            }
            //Seta os valores de cores do vetor de cores dos indicadores
            public int[] IndicatorColors
            {
                set { mIndicatorColors = value; }
            }
            //Seta os valores de cores do vetor de cores dos divisores
            public int[] DividerColors
            {
                set { mDividerColors = value; }
            }
        }
    }
}