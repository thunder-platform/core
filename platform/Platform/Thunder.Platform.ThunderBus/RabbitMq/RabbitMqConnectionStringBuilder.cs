using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using RabbitMQ.Client;

namespace Thunder.Platform.ThunderBus.RabbitMq
{
    public class RabbitMqConnectionStringBuilder : DbConnectionStringBuilder
    {
        private const string VirtualHostKey = "VirtualHost";
        private const string UsernameKey = "Username";
        private const string PasswordKey = "Password";
        private const string NodesKey = "Nodes";
        private const string ExchangeTypeKey = "ExchangeType";
        private const string ExchangeNameKey = "ExchangeName";

        public RabbitMqConnectionStringBuilder()
        {
            SetDefaults();
        }

        public RabbitMqConnectionStringBuilder(string connectionString)
        {
            ConnectionString = connectionString;
            SetDefaults();
        }

        public string VirtualHost
        {
            get => (string)this[VirtualHostKey];
            set => this[VirtualHostKey] = value;
        }

        public string Username
        {
            get => (string)this[UsernameKey];
            set => this[UsernameKey] = value;
        }

        public string Password
        {
            get => (string)this[PasswordKey];
            set => this[PasswordKey] = value;
        }

        public string ExchangeName
        {
            get => (string)this[ExchangeNameKey];
            set => this[ExchangeNameKey] = value;
        }

        public string ExchangeType
        {
            get => (string)this[ExchangeTypeKey];
            set => this[ExchangeTypeKey] = value;
        }

        public string Host
        {
            get
            {
                var defaultHost = Nodes.FirstOrDefault();
                var hostedUrl = defaultHost != null ? defaultHost.ToString() : string.Empty;
                return $"{hostedUrl}/{VirtualHost}";
            }
        }

        public IEnumerable<AmqpTcpEndpoint> Nodes
        {
            get => ((string)this[NodesKey] ?? string.Empty).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(ToAmqpEndpoint);
            set => this[NodesKey] = string.Join(",", value.Select(endpoint => endpoint.ToString()));
        }

        protected void SetDefaults()
        {
            SetDefaultValue(VirtualHostKey, "/");
            SetDefaultValue(UsernameKey, "guest");
            SetDefaultValue(PasswordKey, "guest");
            SetDefaultValue(NodesKey, "amqp://localhost");
            SetDefaultValue(ExchangeTypeKey, "topic");
            SetDefaultValue(ExchangeName, "thunder");
        }

        private static AmqpTcpEndpoint ToAmqpEndpoint(string host)
        {
            var uri = new Uri(host.Trim());
            return new AmqpTcpEndpoint(uri);
        }

        private void SetDefaultValue(string key, string value)
        {
            if (!ContainsKey(key))
            {
                Add(key, value);
            }
        }
    }
}
