using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;

namespace Outraging
{
    public partial class Mainmenu : Form
    {
        Thread campaign_pic_swap;
        Thread campaign_on;
        static string[] difficult = new string[] { "", "钢铁洪流", "人海冲锋", "优势火力", "均衡" };
        static string[] environment = new string[] { "", "森林", "跨河", "城市", "山地", "平原" };
        static string[] campaign = new string[] { "卡昂", "圣洛", "切尔卡瑟", "桑多梅日","库尔斯克","基辅","日托米尔","维捷布斯克", "阿登", "鲁尔","易北河","科隆","慕尼黑","法兰克福","布鲁塞尔","色当","里尔", "莱比锡", "德累斯顿", "布列斯特-维尔纽斯" };
        Random random = new Random();
        Entity playerEntity, enemyEntity;
        Entity playerEntity_backup, enemyEntity_backup;
        int envir = 1, diffi = 1;
        bool GameStop;
        SoundPlayer soundPlayer;
        String msc_main_url = @"..\..\resources\msc\main.wav";
        String msc_end_url = @"..\..\resources\msc\end.wav";
        String msc_start_url = @"..\..\resources\msc\start.wav";
        String msc_on_url = @"..\..\resources\msc\on.wav";
        String msc_allgone_url = @"..\..\resources\msc\allgone.wav";
        public Mainmenu()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer(msc_main_url);
            soundPlayer.Play();
            playerEntity = new Entity(0);
            enemyEntity = new Entity(1);
            this.unit1_num.Text = playerEntity.getArmy(1).ToString();
            this.unit2_num.Text = playerEntity.getArmy(2).ToString();
            this.unit3_num.Text = playerEntity.getArmy(3).ToString();
            this.unit4_num.Text = playerEntity.getArmy(4).ToString();
            this.unit5_num.Text = playerEntity.getArmy(5).ToString();
            this.unit6_num.Text = playerEntity.getArmy(6).ToString();
            this.unit7_num.Text = playerEntity.getArmy(7).ToString();
            this.unit8_num.Text = playerEntity.getArmy(8).ToString();
            this.unit9_num.Text = playerEntity.getArmy(9).ToString();
            this.unit10_num.Text = playerEntity.getArmy(10).ToString();
            this.unit11_num.Text = playerEntity.getArmy(11).ToString();
            this.unit12_num.Text = playerEntity.getArmy(12).ToString();
            this.unit1_num_e.Text = enemyEntity.getArmy(1).ToString();
            this.unit2_num_e.Text = enemyEntity.getArmy(2).ToString();
            this.unit3_num_e.Text = enemyEntity.getArmy(3).ToString();
            this.unit4_num_e.Text = enemyEntity.getArmy(4).ToString();
            this.unit5_num_e.Text = enemyEntity.getArmy(5).ToString();
            this.unit6_num_e.Text = enemyEntity.getArmy(6).ToString();
            this.unit7_num_e.Text = enemyEntity.getArmy(7).ToString();
            this.unit8_num_e.Text = enemyEntity.getArmy(8).ToString();
            this.unit9_num_e.Text = enemyEntity.getArmy(9).ToString();
            this.unit10_num_e.Text = enemyEntity.getArmy(10).ToString();
            this.unit11_num_e.Text = enemyEntity.getArmy(11).ToString();
            this.unit12_num_e.Text = enemyEntity.getArmy(12).ToString();
            this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            this.description_e.Text = "地面部队人力：" + enemyEntity.getAttribute("manpower") + "\r\n组织度：" + enemyEntity.getAttribute("organize") + "\r\n";
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0?"Player":"AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] +"\r\n";
        }

        private void Mainmenu_Load(object sender, EventArgs e)
        {
            this.helpmenu.Hide();
            this.battlemenu.Hide();
            this.OnWarMenu.Hide();
            this.afterwar_bg.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mainmenuOption.Hide();
            this.battlemenu.Show();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(msc_start_url);
            soundPlayer.PlayLooping();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.battlemenu.Hide();
            this.mainmenuOption.Show();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(msc_main_url);
            soundPlayer.PlayLooping();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.helpmenu.Show();
            this.battlemenuOption.Hide();
        }

        private void unit1_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if( this.unit1_num.Text != "")
                    playerEntity.setArmy(1, Convert.ToDouble(this.unit1_num.Text));
                else
                {
                    playerEntity.setArmy(1, 0);
                    this.unit1_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit1_num.Text = playerEntity.getArmy(1).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit1_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(1, Convert.ToDouble(this.unit1_num.Text)+1);
                this.unit1_num.Text=Convert.ToString(Convert.ToDouble(this.unit1_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void Unit1_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(1, Convert.ToDouble(this.unit1_num.Text) - 1);
                this.unit1_num.Text = Convert.ToString(Convert.ToDouble(this.unit1_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void Unit2_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(2, Convert.ToDouble(this.unit2_num.Text) - 1);
                this.unit2_num.Text = Convert.ToString(Convert.ToDouble(this.unit2_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit2_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit2_num.Text != "")
                    playerEntity.setArmy(2, Convert.ToDouble(this.unit2_num.Text));
                else
                {
                    playerEntity.setArmy(2, 0);
                    this.unit2_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit2_num.Text = playerEntity.getArmy(2).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit2_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(2, Convert.ToDouble(this.unit2_num.Text) + 2);
                this.unit2_num.Text = Convert.ToString(Convert.ToDouble(this.unit2_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit3_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(3, Convert.ToDouble(this.unit3_num.Text) + 1);
                this.unit3_num.Text = Convert.ToString(Convert.ToDouble(this.unit3_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit4_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(4, Convert.ToDouble(this.unit4_num.Text) + 1);
                this.unit4_num.Text = Convert.ToString(Convert.ToDouble(this.unit4_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit5_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(5, Convert.ToDouble(this.unit5_num.Text) + 1);
                this.unit5_num.Text = Convert.ToString(Convert.ToDouble(this.unit5_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit6_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(6, Convert.ToDouble(this.unit6_num.Text) + 1);
                this.unit6_num.Text = Convert.ToString(Convert.ToDouble(this.unit6_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit7_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(7, Convert.ToDouble(this.unit7_num.Text) + 1);
                this.unit7_num.Text = Convert.ToString(Convert.ToDouble(this.unit7_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit8_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(8, Convert.ToDouble(this.unit8_num.Text) + 1);
                this.unit8_num.Text = Convert.ToString(Convert.ToDouble(this.unit8_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit9_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(9, Convert.ToDouble(this.unit9_num.Text) + 1);
                this.unit9_num.Text = Convert.ToString(Convert.ToDouble(this.unit9_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit10_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(10, Convert.ToDouble(this.unit10_num.Text) + 1);
                this.unit10_num.Text = Convert.ToString(Convert.ToDouble(this.unit10_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit11_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(11, Convert.ToDouble(this.unit11_num.Text) + 1);
                this.unit11_num.Text = Convert.ToString(Convert.ToDouble(this.unit11_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit12_plus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(12, Convert.ToDouble(this.unit12_num.Text) + 1);
                this.unit12_num.Text = Convert.ToString(Convert.ToDouble(this.unit12_num.Text) + 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit3_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(3, Convert.ToDouble(this.unit3_num.Text) - 1);
                this.unit3_num.Text = Convert.ToString(Convert.ToDouble(this.unit3_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit4_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(4, Convert.ToDouble(this.unit4_num.Text) - 1);
                this.unit4_num.Text = Convert.ToString(Convert.ToDouble(this.unit4_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit5_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(5, Convert.ToDouble(this.unit5_num.Text) - 1);
                this.unit5_num.Text = Convert.ToString(Convert.ToDouble(this.unit5_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit6_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(6, Convert.ToDouble(this.unit6_num.Text) - 1);
                this.unit6_num.Text = Convert.ToString(Convert.ToDouble(this.unit6_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit7_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(7, Convert.ToDouble(this.unit7_num.Text) - 1);
                this.unit7_num.Text = Convert.ToString(Convert.ToDouble(this.unit7_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit8_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(8, Convert.ToDouble(this.unit8_num.Text) - 1);
                this.unit8_num.Text = Convert.ToString(Convert.ToDouble(this.unit8_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit9_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(9, Convert.ToDouble(this.unit9_num.Text) - 1);
                this.unit9_num.Text = Convert.ToString(Convert.ToDouble(this.unit9_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit10_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(10, Convert.ToDouble(this.unit10_num.Text) - 1);
                this.unit10_num.Text = Convert.ToString(Convert.ToDouble(this.unit10_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit11_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(11, Convert.ToDouble(this.unit11_num.Text) - 1);
                this.unit11_num.Text = Convert.ToString(Convert.ToDouble(this.unit11_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit12_minus_Click(object sender, EventArgs e)
        {
            try
            {
                playerEntity.setArmy(12, Convert.ToDouble(this.unit12_num.Text) - 1);
                this.unit12_num.Text = Convert.ToString(Convert.ToDouble(this.unit12_num.Text) - 1);
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit3_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit3_num.Text != "")
                    playerEntity.setArmy(3, Convert.ToDouble(this.unit3_num.Text));
                else
                {
                    playerEntity.setArmy(3, 0);
                    this.unit3_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit3_num.Text = playerEntity.getArmy(3).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit4_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit4_num.Text != "")
                    playerEntity.setArmy(4, Convert.ToDouble(this.unit4_num.Text));
                else
                {
                    playerEntity.setArmy(4, 0);
                    this.unit4_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit4_num.Text = playerEntity.getArmy(4).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit5_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit5_num.Text != "")
                    playerEntity.setArmy(5, Convert.ToDouble(this.unit5_num.Text));
                else
                {
                    playerEntity.setArmy(5, 0);
                    this.unit5_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit5_num.Text = playerEntity.getArmy(5).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit6_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit6_num.Text != "")
                    playerEntity.setArmy(6, Convert.ToDouble(this.unit6_num.Text));
                else
                {
                    playerEntity.setArmy(6, 0);
                    this.unit6_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit6_num.Text = playerEntity.getArmy(6).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit7_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit7_num.Text != "")
                    playerEntity.setArmy(7, Convert.ToDouble(this.unit7_num.Text));
                else
                {
                    playerEntity.setArmy(7, 0);
                    this.unit7_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit7_num.Text = playerEntity.getArmy(7).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit8_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit8_num.Text != "")
                    playerEntity.setArmy(8, Convert.ToDouble(this.unit8_num.Text));
                else
                {
                    playerEntity.setArmy(8, 0);
                    this.unit8_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit8_num.Text = playerEntity.getArmy(8).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit9_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit9_num.Text != "")
                    playerEntity.setArmy(9, Convert.ToDouble(this.unit9_num.Text));
                else
                {
                    playerEntity.setArmy(9, 0);
                    this.unit9_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit9_num.Text = playerEntity.getArmy(9).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit10_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit10_num.Text != "")
                    playerEntity.setArmy(10, Convert.ToDouble(this.unit10_num.Text));
                else
                {
                    playerEntity.setArmy(10, 0);
                    this.unit10_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit10_num.Text = playerEntity.getArmy(10).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit11_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit11_num.Text != "")
                    playerEntity.setArmy(11, Convert.ToDouble(this.unit11_num.Text));
                else
                {
                    playerEntity.setArmy(11, 0);
                    this.unit11_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit11_num.Text = playerEntity.getArmy(11).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void unit12_num_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.unit12_num.Text != "")
                    playerEntity.setArmy(12, Convert.ToDouble(this.unit12_num.Text));
                else
                {
                    playerEntity.setArmy(12, 0);
                    this.unit12_num.Text = "0";
                }
                this.description.Text = "剩余产能：" + playerEntity.getAttribute("production") + "\r\n战场宽度：" + playerEntity.getAttribute("warwidth") + "\r\n地面部队人力：" + playerEntity.getAttribute("manpower") + "\r\n组织度：" + playerEntity.getAttribute("organize") + "\r\n";
            }
            catch (Exception ex)
            {
                this.unit12_num.Text = playerEntity.getArmy(12).ToString();
                MessageBox.Show(Text, ex.Message);
            }
        }

        private void difficulty1_Click(object sender, EventArgs e)
        {
            enemyEntity.setEnemy(1); diffi = 1;
            this.unit1_num_e.Text = enemyEntity.getArmy(1).ToString();
            this.unit2_num_e.Text = enemyEntity.getArmy(2).ToString();
            this.unit3_num_e.Text = enemyEntity.getArmy(3).ToString();
            this.unit4_num_e.Text = enemyEntity.getArmy(4).ToString();
            this.unit5_num_e.Text = enemyEntity.getArmy(5).ToString();
            this.unit6_num_e.Text = enemyEntity.getArmy(6).ToString();
            this.unit7_num_e.Text = enemyEntity.getArmy(7).ToString();
            this.unit8_num_e.Text = enemyEntity.getArmy(8).ToString();
            this.unit9_num_e.Text = enemyEntity.getArmy(9).ToString();
            this.unit10_num_e.Text = enemyEntity.getArmy(10).ToString();
            this.unit11_num_e.Text = enemyEntity.getArmy(11).ToString();
            this.unit12_num_e.Text = enemyEntity.getArmy(12).ToString();
            description_e.Text = "地面部队人力：" + enemyEntity.getAttribute("manpower") + "\r\n组织度：" + enemyEntity.getAttribute("organize") + "\r\n";
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void difficulty2_Click(object sender, EventArgs e)
        {
            enemyEntity.setEnemy(2); diffi = 2;
            this.unit1_num_e.Text = enemyEntity.getArmy(1).ToString();
            this.unit2_num_e.Text = enemyEntity.getArmy(2).ToString();
            this.unit3_num_e.Text = enemyEntity.getArmy(3).ToString();
            this.unit4_num_e.Text = enemyEntity.getArmy(4).ToString();
            this.unit5_num_e.Text = enemyEntity.getArmy(5).ToString();
            this.unit6_num_e.Text = enemyEntity.getArmy(6).ToString();
            this.unit7_num_e.Text = enemyEntity.getArmy(7).ToString();
            this.unit8_num_e.Text = enemyEntity.getArmy(8).ToString();
            this.unit9_num_e.Text = enemyEntity.getArmy(9).ToString();
            this.unit10_num_e.Text = enemyEntity.getArmy(10).ToString();
            this.unit11_num_e.Text = enemyEntity.getArmy(11).ToString();
            this.unit12_num_e.Text = enemyEntity.getArmy(12).ToString();
            description_e.Text = "地面部队人力：" + enemyEntity.getAttribute("manpower") + "\r\n组织度：" + enemyEntity.getAttribute("organize") + "\r\n";
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void difficulty3_Click(object sender, EventArgs e)
        {
            enemyEntity.setEnemy(3); diffi = 3;
            this.unit1_num_e.Text = enemyEntity.getArmy(1).ToString();
            this.unit2_num_e.Text = enemyEntity.getArmy(2).ToString();
            this.unit3_num_e.Text = enemyEntity.getArmy(3).ToString();
            this.unit4_num_e.Text = enemyEntity.getArmy(4).ToString();
            this.unit5_num_e.Text = enemyEntity.getArmy(5).ToString();
            this.unit6_num_e.Text = enemyEntity.getArmy(6).ToString();
            this.unit7_num_e.Text = enemyEntity.getArmy(7).ToString();
            this.unit8_num_e.Text = enemyEntity.getArmy(8).ToString();
            this.unit9_num_e.Text = enemyEntity.getArmy(9).ToString();
            this.unit10_num_e.Text = enemyEntity.getArmy(10).ToString();
            this.unit11_num_e.Text = enemyEntity.getArmy(11).ToString();
            this.unit12_num_e.Text = enemyEntity.getArmy(12).ToString();
            description_e.Text = "地面部队人力：" + enemyEntity.getAttribute("manpower") + "\r\n组织度：" + enemyEntity.getAttribute("organize") + "\r\n";
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void env_1_Click(object sender, EventArgs e)
        {
            envir = 1;
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void env_2_Click(object sender, EventArgs e)
        {
            envir = 2;
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void env_3_Click(object sender, EventArgs e)
        {
            envir = 3;
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void env_4_Click(object sender, EventArgs e)
        {
            envir = 4;
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void env_5_Click(object sender, EventArgs e)
        {
            envir = 5;
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void swap_team_Click(object sender, EventArgs e)
        {
            enemyEntity.swapSide();playerEntity.swapSide();
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void OnWarReturn_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(msc_start_url);
            soundPlayer.PlayLooping();
            campaign_pic_swap.Abort();
            this.campaign_on.Abort();
            OnWarMenu.Hide();
        }

        private void OnWarPause_Click(object sender,EventArgs e)
        {
            GameStop = GameStop == true ? false : true;
            this.OnWarPause.Text = GameStop == true ? "Continue" : "Pause";
            if (GameStop == true) campaign_on.Suspend();
            else campaign_on.Resume();
        }

        private void start_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            playerEntity_backup = new Entity(playerEntity,0);
            enemyEntity_backup = new Entity(enemyEntity, 1);
            soundPlayer = new SoundPlayer(msc_on_url);
            soundPlayer.PlayLooping();
            OnWarMenu.Show();
            this.OnWarOption.Show();
            GameStop = false;
            this.campaign_name.Text = campaign[random.Next(10)] + "战役";
            campaign_pic_swap = new Thread(
                delegate ()
                {
                    for(int i = random.Next(10);;i = random.Next(10))
                    {
                        if (i == 0) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar0;
                        else if (i == 1) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar1;
                        else if (i == 2) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar2;
                        else if (i == 3) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar3;
                        else if (i == 4) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar4;
                        else if (i == 5) this.campaign_img.Image = global::Outraging.Properties.Resources.owwar5;
                        else if (i == 6) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar6;
                        else if (i == 7) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar7;
                        else if (i == 8) this.campaign_img.Image = global::Outraging.Properties.Resources.onwar8;
                        else this.campaign_img.Image = global::Outraging.Properties.Resources.onwar9;
                        Thread.Sleep(1000); 
                    }
                }
            );
            campaign_pic_swap.Start();
            campaign_refresh(0);
            campaign_on = new Thread(
                delegate ()
                {
                    while (playerEntity_backup.getAttribute("organize")!=0&&enemyEntity_backup.getAttribute("organize") !=0&& playerEntity_backup.getAttribute("manpower") != 0&& enemyEntity_backup.getAttribute("organize") != 0)
                    {

                        Thread.Sleep(1500);
                        //this is where the algorithm needs to be.
                        double attack = Calc.calculate_attack(playerEntity_backup,envir); double enattack = Calc.calculate_attack(enemyEntity_backup,envir);
                        double hard_attack = Calc.calculate_hard(playerEntity_backup,envir); double enhard_attack = Calc.calculate_hard(enemyEntity_backup,envir);
                        double defence = Calc.calculate_defence(playerEntity_backup,envir); double endefence = Calc.calculate_defence(enemyEntity_backup,envir);
                        double breach = Calc.calculate_breach(playerEntity_backup,envir); double enbreach = Calc.calculate_breach(enemyEntity_backup,envir);
                        double antiair = Calc.calculate_antiair(playerEntity_backup,envir); double enantiair = Calc.calculate_antiair(enemyEntity_backup, envir);
                        double armor = Calc.calculate_armor(playerEntity_backup,envir); double enarmor = Calc.calculate_armor(enemyEntity_backup, envir);
                        playerEntity_backup.setAttribute("organize",((int)((playerEntity_backup.getAttribute("organize") - Calc.calculate_organize(breach, antiair, armor, playerEntity_backup.getArmy("fig"), enattack, enhard_attack, enemyEntity_backup.getArmy("atc"))) * 10) / 10));
                        enemyEntity_backup.setAttribute("organize",((int)((enemyEntity_backup.getAttribute("organize") - Calc.calculate_organize(endefence, enantiair, enarmor, enemyEntity_backup.getArmy("fig"), attack, hard_attack, playerEntity_backup.getArmy("atc"))) * 10) / 10 - 1));
                        playerEntity_backup.setAttribute("manpower",Calc.calculate_manpower(playerEntity_backup.getAttribute("manpower"), breach, antiair, armor, playerEntity_backup.getArmy("fig"), enattack, enhard_attack, enemyEntity_backup.getArmy("atc")));
                        enemyEntity_backup.setAttribute("manpower",Calc.calculate_manpower(enemyEntity_backup.getAttribute("manpower"), endefence, enantiair, enarmor, enemyEntity_backup.getArmy("fig"), attack, hard_attack, playerEntity_backup.getArmy("atc")));
                        //
                        campaign_refresh(1);
                    }
                    campaign_results();
                }
            );
            campaign_on.Start();
        }

        private void campaign_results()
        {
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(msc_end_url);
            soundPlayer.Play();
            if ((playerEntity_backup.getAttribute("organize") == 0|| playerEntity_backup.getAttribute("manpower") == 0) && (enemyEntity_backup.getAttribute("organize") != 0&& enemyEntity_backup.getAttribute("manpower") != 0))
            {
                this.afterwar_text.Text = "Defeat";
                this.afterwar_img.Image = global::Outraging.Properties.Resources.Ending_1;
            }
            else if ((playerEntity_backup.getAttribute("organize") != 0&& playerEntity_backup.getAttribute("manpower") != 0) && (enemyEntity_backup.getAttribute("organize") == 0|| enemyEntity_backup.getAttribute("manpower") == 0))
            {
                this.afterwar_text.Text = "Victory";
                this.afterwar_img.Image = global::Outraging.Properties.Resources.Ending_2;
            }
            else
            {
                this.afterwar_text.Text = "ALL GONE";
                this.afterwar_img.Image = global::Outraging.Properties.Resources.Ending_3;
                soundPlayer.Stop();
                soundPlayer = new SoundPlayer(msc_allgone_url);
                soundPlayer.Play();
            }
            this.OnWarOption.Hide();
            this.afterwar_bg.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.campaign_pic_swap.Abort();
            this.afterwar_bg.Hide();
            this.OnWarMenu.Hide();
            soundPlayer.Stop();
            soundPlayer = new SoundPlayer(msc_start_url);
            soundPlayer.PlayLooping();
        }

        private void easter_egg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("唯一官方网站:https://www.bilibili.com/video/BV1GJ411x7h7");
        }

        private void campaign_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void afterwar_bg_Paint(object sender, PaintEventArgs e)
        {

        }

        private void difficulty4_Click(object sender, EventArgs e)
        {
            enemyEntity.setEnemy(4);diffi = 4;
            this.unit1_num_e.Text = enemyEntity.getArmy(1).ToString();
            this.unit2_num_e.Text = enemyEntity.getArmy(2).ToString();
            this.unit3_num_e.Text = enemyEntity.getArmy(3).ToString();
            this.unit4_num_e.Text = enemyEntity.getArmy(4).ToString();
            this.unit5_num_e.Text = enemyEntity.getArmy(5).ToString();
            this.unit6_num_e.Text = enemyEntity.getArmy(6).ToString();
            this.unit7_num_e.Text = enemyEntity.getArmy(7).ToString();
            this.unit8_num_e.Text = enemyEntity.getArmy(8).ToString();
            this.unit9_num_e.Text = enemyEntity.getArmy(9).ToString();
            this.unit10_num_e.Text = enemyEntity.getArmy(10).ToString();
            this.unit11_num_e.Text = enemyEntity.getArmy(11).ToString();
            this.unit12_num_e.Text = enemyEntity.getArmy(12).ToString();
            description_e.Text = "地面部队人力：" + enemyEntity.getAttribute("manpower") + "\r\n组织度：" + enemyEntity.getAttribute("organize") + "\r\n";
            this.total_describe.Text = "战争总况\r\n防守方：" + (playerEntity.getSide() == 0 ? "Player" : "AI") + "\r\n进攻方：" + (playerEntity.getSide() == 1 ? "Player" : "AI") + "\r\nAI战术:" + difficult[diffi] + "\r\n作战环境：" + environment[envir] + "\r\n";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.helpmenu.Hide();
            this.battlemenuOption.Show();
        }

        private void campaign_refresh(int arg)
        {
            if (arg == 0)
            {
                this.onwar_unit1_num.Text = playerEntity_backup.getArmy(1).ToString();
                this.onwar_unit2_num.Text = playerEntity_backup.getArmy(2).ToString();
                this.onwar_unit3_num.Text = playerEntity_backup.getArmy(3).ToString();
                this.onwar_unit4_num.Text = playerEntity_backup.getArmy(4).ToString();
                this.onwar_unit5_num.Text = playerEntity_backup.getArmy(5).ToString();
                this.onwar_unit6_num.Text = playerEntity_backup.getArmy(6).ToString();
                this.onwar_unit7_num.Text = playerEntity_backup.getArmy(7).ToString();
                this.onwar_unit8_num.Text = playerEntity_backup.getArmy(8).ToString();
                this.onwar_unit9_num.Text = playerEntity_backup.getArmy(9).ToString();
                this.onwar_unit10_num.Text = playerEntity_backup.getArmy(10).ToString();
                this.onwar_unit11_num.Text = playerEntity_backup.getArmy(11).ToString();
                this.onwar_unit12_num.Text = playerEntity_backup.getArmy(12).ToString();
                this.onwar_unit1_num_e.Text = enemyEntity_backup.getArmy(1).ToString();
                this.onwar_unit2_num_e.Text = enemyEntity_backup.getArmy(2).ToString();
                this.onwar_unit3_num_e.Text = enemyEntity_backup.getArmy(3).ToString();
                this.onwar_unit4_num_e.Text = enemyEntity_backup.getArmy(4).ToString();
                this.onwar_unit5_num_e.Text = enemyEntity_backup.getArmy(5).ToString();
                this.onwar_unit6_num_e.Text = enemyEntity_backup.getArmy(6).ToString();
                this.onwar_unit7_num_e.Text = enemyEntity_backup.getArmy(7).ToString();
                this.onwar_unit8_num_e.Text = enemyEntity_backup.getArmy(8).ToString();
                this.onwar_unit9_num_e.Text = enemyEntity_backup.getArmy(9).ToString();
                this.onwar_unit10_num_e.Text = enemyEntity_backup.getArmy(10).ToString();
                this.onwar_unit11_num_e.Text = enemyEntity_backup.getArmy(11).ToString();
                this.onwar_unit12_num_e.Text = enemyEntity_backup.getArmy(12).ToString();
            }
            this.onwar_description.Text = "地面部队人力：" + playerEntity_backup.getAttribute("manpower") + "\r\n组织度：" + playerEntity_backup.getAttribute("organize") + "\r\n";
            this.onwar_description_e.Text = "地面部队人力：" + enemyEntity_backup.getAttribute("manpower") + "\r\n组织度：" + enemyEntity_backup.getAttribute("organize") + "\r\n";
            if (playerEntity_backup.getAttribute("organize") != 0 || enemyEntity_backup.getAttribute("organize") != 0)
            {
                player_bg_ONWAR.Location = new System.Drawing.Point(-1630 + Convert.ToInt32(1630 * (Convert.ToDouble(playerEntity_backup.getAttribute("organize")) / (Convert.ToDouble(playerEntity_backup.getAttribute("organize")) + Convert.ToDouble(enemyEntity_backup.getAttribute("organize"))))), 0);
                Enemy_bg_ONWAR.Location = new System.Drawing.Point(Convert.ToInt32(1630 * (Convert.ToDouble(playerEntity_backup.getAttribute("organize")) / (Convert.ToDouble(playerEntity_backup.getAttribute("organize")) + Convert.ToDouble(enemyEntity_backup.getAttribute("organize"))))), 0);
            }
            else
            {
                player_bg_ONWAR.Location = new System.Drawing.Point(-1630 , 0);
                Enemy_bg_ONWAR.Location = new System.Drawing.Point(1630 , 0);
            }
        }
    }
}
