﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUI.Reader.Model
{
    // A vous de completer ce qu'est un Livre !!
    // /!\ ATTENTION ! Si vous récupéré les livres depuis votre serveur, cette classe ne sert plus a rien !
    public class Book
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public float Price { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
