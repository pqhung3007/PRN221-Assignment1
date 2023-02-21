using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for ProfileManagement.xaml
    /// </summary>
    public partial class ProfileManagement : Window
    {
        private IProductRepository productRepository;
        private IMemberRepository memberRepository;
        private IOrderRepository orderRepository;
        private string email;
        private string password;

        public ProfileManagement(IProductRepository productRepository, IMemberRepository memberRepository, IOrderRepository orderRepository, string email, string password)
        {
            InitializeComponent();
            this.memberRepository = memberRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.email = email;
            this.password = password;
        }
    }
}
