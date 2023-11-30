using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WinCOM
{
    public partial class Form1 : Form
    {
        string RxString;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = cbPortas.Items[cbPortas.SelectedIndex].ToString();
                    serialPort1.Open();
                }
                catch
                {
                    return;
                }
                if (serialPort1.IsOpen)
                {
                    btnConectar.Text = "Desconectar";
                    cbPortas.Enabled = false;
                    txtEnviar.Focus();
                }
            }
            else
            {
                try
                {
                    serialPort1.Close();
                    cbPortas.Enabled = true;
                    btnConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)          //porta está aberta
                serialPort1.Write(txtEnviar.Text);  //envia o texto presente no textbox Enviar
        }
        private void atualizaListaCOMs()
        {
            int i = 0;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou             
            quantDiferente = false;
            //se a quantidade de portas mudou
            if (cbPortas.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (cbPortas.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }
            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }
            //limpa comboBox
            cbPortas.Items.Clear();
            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                cbPortas.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            cbPortas.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void cbPortas_Click(object sender, EventArgs e)
        {
            
        }

        private void timerCOM_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)  // se porta aberta
                serialPort1.Close();            //fecha a porta
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = serialPort1.ReadExisting();              //le o dado disponível na serial
            this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebido(object sender, EventArgs e)
        {
            txtReceber.AppendText(RxString);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtReceber.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void txtEnviar_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtEnviar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //tecla 0 pressionada
                btnEnviar.PerformClick();
                e.Handled = true;
            }            
        }
    }
    
}
