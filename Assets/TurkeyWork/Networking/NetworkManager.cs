using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Networking {

    [BoltGlobalBehaviour]
    public class NetworkManager : Bolt.GlobalEventListener {

        static NetworkManager instance;

        List<NetworkPlayer> players;

        public static bool InGame { get; private set; }

        NetworkPlayer serverPlayer;

        private void Awake () {
            if (instance == null) {
                instance = this;
                players = new List<NetworkPlayer> ();
            }
        }

        public static NetworkPlayer CreateServerPlayer () {
            return CreatePlayer (null);
        }

        public static NetworkPlayer CreateClientPlayer (BoltConnection connection) {
            return CreatePlayer (connection);
        }

        static NetworkPlayer CreatePlayer (BoltConnection connection) {
            var player = new NetworkPlayer () {
                Connection = connection
            };

            if (player.IsClient)
                player.Connection.UserData = connection;
            else
                instance.serverPlayer = player;

            instance.players.Add (player);
            return player;
        }

        public static NetworkPlayer[] GetAllPlayers () {
            return instance.players.ToArray ();
        }

        public static NetworkPlayer GetPlayer (BoltConnection connection) {
            if (connection == null)
                return instance.serverPlayer;
            return instance.players.Find (player => player.Connection == connection);
        }

        public override void BoltStartDone () {
            InGame = true;
            if (BoltNetwork.isServer) {
                BoltNetwork.LoadScene ("Test-Level");
            } else {
                BoltNetwork.Connect (UdpKit.UdpEndPoint.Parse ("127.0.0.1:27000"));
            }
        }
    }
}