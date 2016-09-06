﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contatos.ViewModel
{
    public class ContatosViewModel : Notify.BaseModel
    {
        public static string SQLitePath =
            Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                "database.sqlite");

        //private static MobileServiceClient mobileService =
        //    new MobileServiceClient("http://studentsintouch.azurewebsites.net",
        //        null,
        //        new Model.AzureHandler());

        private IMobileServiceTable<Model.Contact> contatoTable { get; set; }

        //private ObservableCollection<Model.Contact> listaContatos;
        //public ObservableCollection<Model.Contact> ListaContatos
        //{
        //    get { return this.listaContatos; }
        //    set { SetField(ref this.listaContatos, value); }
        //}
        private MobileServiceCollection<Model.Contact, Model.Contact> listaContatos;
        public MobileServiceCollection<Model.Contact, Model.Contact> ListaContatos
        {
            get { return this.listaContatos; }
            set { SetField(ref this.listaContatos, value); }
        }

        private ObservableCollection<Model.Contact> listaContatosFavoritos;
        public ObservableCollection<Model.Contact> ListaContatosFavoritos
        {
            get { return this.listaContatosFavoritos; }
            set { SetField(ref this.listaContatosFavoritos, value); }
        }

        private Model.Contact selectedContato;
        public Model.Contact SelectedContato
        {
            get { return this.selectedContato; }
            set { SetField(ref this.selectedContato, value); }
        }

        public ContatosViewModel()
        {
            //using (SQLite.Net.SQLiteConnection connection =
            //    new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), SQLitePath))
            //{
            //    connection.CreateTable<Model.Contato>();

            //    this.ListaContatos =
            //        new ObservableCollection<Model.Contato>
            //        (connection.Table<Model.Contato>());

            //    this.ListaContatosFavoritos =
            //        new ObservableCollection<Model.Contato>
            //        (ListaContatos.Where(c => c.IsFavorito));
            //}

            this.contatoTable = App.mobileService.GetTable<Model.Contact>();

            this.IncluirCommand = new Commands.ActionCommand(Incluir);
            this.SalvarCommand = new Commands.ActionCommand(SalvarAsync);
            this.EditarCommand = new Commands.ActionCommand(Editar);
            this.ApagarCommand = new Commands.ActionCommand(ApagarAsync);
            this.CancelarCommand = new Commands.ActionCommand(Cancelar);
        }

        public async Task GetAllContatosAsync()
        {
            //Uri url = new Uri("http://studentsintouch.azurewebsites.net/tables/Contact");
            //var method = new Windows.Web.Http.HttpMethod("GET");
            //var request = new Windows.Web.Http.HttpRequestMessage(method, url);

            //string json = await Model.Conector
            //    .SendRequestAsync(request);

            //var contatos = Newtonsoft.Json.JsonConvert
            //    .DeserializeObject<List<Model.Contact>>(json);

            //this.ListaContatos =
            //    new ObservableCollection<Model.Contact>(contatos);
            //this.ListaContatosFavoritos =
            //    new ObservableCollection<Model.Contact>(contatos.Where(c => c.IsFavorito));

            try
            {
                this.ListaContatos = await contatoTable.ToCollectionAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public ICommand IncluirCommand { get; private set; }

        private void Incluir()
        {
            this.SelectedContato = new Model.Contact();
            App.RootFrame.Navigate(typeof(Contato), this);
        }

        public ICommand EditarCommand { get; private set; }
        private void Editar()
        {
            App.RootFrame.Navigate(typeof(Contato), this);
        }

        public ICommand SalvarCommand { get; private set; }
        private async void SalvarAsync()
        {
            //using (SQLite.Net.SQLiteConnection connection =
            //    new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), SQLitePath))
            //{
            //    connection.InsertOrReplace(this.SelectedContato);
            //}

            //Newtonsoft.Json.JsonSerializerSettings settings =
            //    new Newtonsoft.Json.JsonSerializerSettings();
            //settings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //settings.NullValueHandling =
            //    Newtonsoft.Json.NullValueHandling.Ignore;

            //var json = Newtonsoft.Json.JsonConvert
            //    .SerializeObject(this.SelectedContato, settings);
            //var contentJson = new Windows.Web.Http
            //    .HttpStringContent(json,
            //    Windows.Storage.Streams.UnicodeEncoding.Utf8,
            //    "application/json");

            //Uri url =
            //    new Uri($"http://studentsintouch.azurewebsites.net/tables/Contact/{this.SelectedContato.ID}");
            //string method = string.IsNullOrWhiteSpace(this.SelectedContato.ID) ?
            //    "POST" : "PATCH";

            //var requestMethod = new Windows.Web.Http.HttpMethod(method);
            //var request = new Windows.Web.Http.HttpRequestMessage(requestMethod, url);
            //request.Content = contentJson;

            //try
            //{
            //    string result = await Model.Conector.SendRequestAsync(request);
            //}
            //catch (Exception e)
            //{

            //}

            try
            {
                if (string.IsNullOrWhiteSpace(this.SelectedContato.ID))
                    await contatoTable.InsertAsync(this.SelectedContato);
                else
                    await contatoTable.UpdateAsync(this.SelectedContato);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            App.RootFrame.GoBack();
        }

        public ICommand ApagarCommand { get; private set; }
        private async void ApagarAsync()
        {
            //using (SQLite.Net.SQLiteConnection connection =
            //    new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), SQLitePath))
            //{
            //    connection.Delete(this.SelectedContato);
            //}

            //Uri url =
            //    new Uri($"http://studentsintouch.azurewebsites.net/tables/Contact/{this.SelectedContato.ID}");

            //var requestMethod = new Windows.Web.Http.HttpMethod("DELETE");
            //var request = new Windows.Web.Http.HttpRequestMessage(requestMethod, url);

            //try
            //{
            //    string result = await Model.Conector.SendRequestAsync(request);
            //}
            //catch (Exception e)
            //{

            //}

            try
            {
                await this.contatoTable.DeleteAsync(this.SelectedContato);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            App.RootFrame.GoBack();
        }

        public ICommand CancelarCommand { get; private set; }
        private void Cancelar()
        {
            this.SelectedContato = null;
            App.RootFrame.GoBack();
        }
    }
}
