using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wheel.DAL.Models;

namespace Wheel.User.Models
{
    public class commonModel
    {
        private indexModel _index = new indexModel();
        private tyreModel _tyres = new tyreModel();
        private tyreModel _saleTyres = new tyreModel();
        private DAL.Context.Pages _pageContent = new DAL.Context.Pages();
        private List<DAL.Context.Pages> _pageContents = new List<DAL.Context.Pages>();
        private List<breadcrumbModel> _breadCrumb = new List<breadcrumbModel>();
        private List<catalogViewModel> _catalog = new List<catalogViewModel>();
        private TyreViewModel _tyreDetails = new TyreViewModel();
        private List<DAL.Context.Car> _carbrand = new List<DAL.Context.Car>();
        private double _currency;
        public double currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        private string _lang;

        public string lang
        {
            get { return _lang; }
            set { _lang = value; }
        }
        public indexModel index
        {
            get { return _index; }
            set { _index = value; }
        }

        public tyreModel tyres
        {
            get { return _tyres; }
            set { _tyres = value; }
        }
        public List<catalogViewModel> catalog
        {
            get { return _catalog; }
            set { _catalog = value; }
        }
        public tyreModel saleTyres
        {
            get { return _saleTyres; }
            set { _saleTyres = value; }
        }
        public List<breadcrumbModel> breadCrumb
        {
            get { return _breadCrumb; }
            set { _breadCrumb = value; }
        }

        public DAL.Context.Pages pageContent
        {
            get { return _pageContent; }
            set { _pageContent = value; }
        }

        public List<DAL.Context.Pages> pageContents
        {
            get { return _pageContents; }
            set { _pageContents = value; }
        }
        public TyreViewModel tyreDetails
        {
            get { return _tyreDetails; }
            set { _tyreDetails = value; }
        }

        public List<DAL.Context.Car> carbrand
        {
            get { return _carbrand; }
            set { _carbrand = value; }
        }

    }
    public class indexModel
    {
        public List<Wheel.DAL.Context.slider> sliders { get; set; }
        public Wheel.DAL.Context.slider slide { get; set; }
    }

    public class tyreModel
    {
        public List<Wheel.DAL.Models.TyreViewModel> tyreList { get; set; }
    }

    public class catalogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DAL.Context.TyreModel> Models { get; set; }
    }

    public class breadcrumbModel
    {
        public string url { get; set; }
        public string style { get; set; }
        public string name { get; set; }
        public bool home { get; set; }
    }

    public class contactModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string body { get; set; }
    }

    public class TyreSearchModel : TyreViewModel
    {
        public int priceMin { get; set; }
        public int priceMax { get; set;}
        public bool? usage { get; set; } = null;
        public string priceArray { get; set; }
        public List<DAL.Context.TyreBrand> brands { get; set; }
        public int campaign { get; set; }
    }
}