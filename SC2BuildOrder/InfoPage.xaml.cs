using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Coding4Fun.Phone.Controls.Data;

namespace SC2BuildOrder
{
    public partial class InfoPage : PhoneApplicationPage
    {
        MarketplaceReviewTask _marketplaceReview = new MarketplaceReviewTask();
        MarketplaceSearchTask _marketplaceSearch = new MarketplaceSearchTask();
        EmailComposeTask _emailComposeTask = new EmailComposeTask();

        public InfoPage()
        {
            InitializeComponent();
            txtHelp.Text = "We will continue to roll out new features and your suggestions.";
            txtAppName.Text = PhoneHelper.GetAppAttribute("Title") + " by " + PhoneHelper.GetAppAttribute("Author");
            txtVersion.Text = "version " + PhoneHelper.GetAppAttribute("Version");
            txtDescription.Text = PhoneHelper.GetAppAttribute("Description");
        }

        private void btnMarketplace_Click(object sender, RoutedEventArgs e)
        {
            _marketplaceSearch.SearchTerms = "PNGC WP7";
            _marketplaceSearch.Show();
        }

        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            _marketplaceReview.Show();
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            _emailComposeTask.To = "pngc.wp7@hotmail.com";
            _emailComposeTask.Subject = "Startcraft 2 Counters Feedback";
            _emailComposeTask.Show();
        }
    }
}