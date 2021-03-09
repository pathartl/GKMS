﻿using GKMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Terminal.Gui;

namespace GKMS.Client
{
    public class Program
    {
        private static Window Window;
        private static FrameView LeftPane;
        private static List<IGame> Games;
        private static ListView GamesListView;
        private static FrameView RightPane;
        private static Label KeyField;
        private static Label KeyLabel;
        private static Button GetKeyButton;
        private static IGame SelectedGame;
        private static Listener Listener;

        private static int GamesListViewItem;
        private static object ipe;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;

            using (Listener = new Listener())
            {
                Application.Init();

                var top = Application.Top;

                Window = new Window("GKMS Client")
                {
                    X = 0,
                    Y = 0,
                    Width = Dim.Fill(),
                    Height = Dim.Fill()
                };

                top.Add(Window);

                InitLeftPane();
                InitRightPane();

                Application.Run();
            }
        }

        private static void InitLeftPane()
        {
            LeftPane = new FrameView("Games")
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(33),
                Height = Dim.Fill(1),
                CanFocus = false
            };

            Games = GetSupportedGameTypes().Select(gt => (IGame)Activator.CreateInstance(gt)).ToList();

            GamesListView = new ListView(Games.Select(g => g.Name).ToList())
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(0),
                Height = Dim.Fill(0),
                AllowsMarking = false,
                CanFocus = true
            };

            GamesListView.OpenSelectedItem += GamesListView_OpenSelectedItem;

            LeftPane.Add(GamesListView);
            Window.Add(LeftPane);
        }

        private static void InitRightPane()
        {
            RightPane = new FrameView("Manage")
            {
                X = Pos.Percent(33),
                Y = 0,
                Width = Dim.Percent(67),
                Height = Dim.Fill(1),
                CanFocus = true
            };

            KeyField = new Label()
            {
                X = 3,
                Y = Pos.Center(),
                Height = 1,
                Width = Dim.Fill(4),
                TextAlignment = TextAlignment.Centered,
                ColorScheme = Colors.Dialog
            };

            KeyLabel = new Label()
            {
                X = 3,
                Y = Pos.Center() - 2,
                Width = Dim.Fill(4),
                Height = 1,
                TextAlignment = TextAlignment.Centered,
                Text = "CD Key:"
            };

            GetKeyButton = new Button()
            {
                X = Pos.Center(),
                Y = Pos.Center() + 2,
                Height = 1,
                Text = "Get Key"
            };

            GetKeyButton.Clicked += GetKeyButton_Clicked;

            RightPane.Add(KeyField);
            RightPane.Add(KeyLabel);
            RightPane.Add(GetKeyButton);

            Window.Add(RightPane);
        }

        private static void GamesListView_OpenSelectedItem(EventArgs e)
        {
            GamesListViewItem = GamesListView.SelectedItem;

            SelectedGame = Games[GamesListView.SelectedItem];

            RightPane.Title = SelectedGame.Name;
            RightPane.SetFocus();

            var currentKey = SelectedGame.GetKey();

            KeyField.Text = String.IsNullOrWhiteSpace(currentKey) ? "No Key Found" : currentKey;

            //Get
        }

        private static void GetKeyButton_Clicked()
        {
            var packet = new Packet()
            {
                Type = PacketType.ServerAllocateKey,
                Message = SelectedGame.GetType().Name
            };

            Listener.Send(packet, new IPEndPoint(IPAddress.Broadcast, 420));
        }

        private static IEnumerable<Type> GetSupportedGameTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IGame).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
        }
    }
}
