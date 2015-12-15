using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;

namespace DiaCont
{
    public class SlidingTabFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }


        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();
            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class SamplePagerAdapter : PagerAdapter
        {
            List<string> items = new List<string>();
            
            public SamplePagerAdapter() : base()
            {
                items.Add("Login");
                items.Add("Nova");
                items.Add("Alterar");
                items.Add("Excluir");
                items.Add("Listar");
            }

            public override int Count
            {
                get{ return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }
            
            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                View view = null;

                if (position == 0)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.login, container, false);
                    container.AddView(view);
                    return view;
                }
                else if (position == 1) {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.nova, container, false);
                    container.AddView(view);
                    return view;
                }
                else if (position == 2)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.alterar, container, false);
                    container.AddView(view);
                    return view;
                }
                else if (position == 3)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.excluir, container, false);
                    container.AddView(view);
                    return view;
                }
                else {
                    return view;
                }
                
                /*view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                container.AddView(view);
                TextView txtTitle = view.FindViewById<TextView>(Resource.Id.item_title);
                 int pos = position + 1;
                 txtTitle.Text = pos.ToString();
                */
                
            }

            public string GetHeaderTitle (int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }

        }
    }
}