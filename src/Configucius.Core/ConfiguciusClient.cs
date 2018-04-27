using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Configucius.Core
{
    public class ConfiguciusClient : IConfigucius
    {
        private readonly string _domain;
        private readonly string _environment;
        private readonly IConfigRepository _configRepository;
        private static ConcurrentDictionary<string, Config> _values;
        private static Timer _timer;

        public ConfiguciusClient(IConfigRepository configRepository, TimeSpan refreshTime)
        {
            _configRepository = configRepository;
            _domain = ConfigurationManager.AppSettings["Configucius_Domain"];
            _environment = ConfigurationManager.AppSettings["Configucius_Environment"];
            _values = GetConfigValues();

            _timer = new Timer(x =>
            {
                RefreshTheConfigCache();
            }, null, (int)refreshTime.TotalMilliseconds, (int)refreshTime.TotalMilliseconds);
           
        }

        private void RefreshTheConfigCache()
        {
            ConcurrentDictionary<string, Config> values = GetConfigValues();

            foreach (KeyValuePair<string, Config> configItem in values) 
            {
                Config currentConfig;
                if(_values.TryGetValue(configItem.Key, out currentConfig))
                {
                    if(currentConfig.UpdatedAt != configItem.Value.UpdatedAt)
                    {
                        _values[configItem.Key] = configItem.Value;
                    }
                }
                else
                {
                    _values.TryAdd(configItem.Key, configItem.Value);
                }
            }
        }

        private ConcurrentDictionary<string, Config> GetConfigValues()
        {
            ConcurrentDictionary<string, Config> values = new ConcurrentDictionary<string, Config>();
            List<Config> configValues = _configRepository.GetValues(_domain, _environment).ToList();

            foreach (Config config in configValues)
            {
                values.TryAdd(config.Key, config);
            }

            return values;
        }

        public T GetValue<T>(string key)
        {
            Config config;
            if(_values.TryGetValue(key, out config))
            {
                return (T)Convert.ChangeType(config.Value, typeof(T));
            }

            throw new ArgumentNullException($"{key} not found.");
        }
    }
}