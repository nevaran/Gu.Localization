﻿namespace Gu.Wpf.Localization
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    /// <summary>
    /// The object that the translation extension binds to
    /// </summary>
    public class TranslationData : IWeakEventListener, ITranslationData
    {
        private readonly TranslationManager _translationManager;
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationData"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="translationManager"></param>
        public TranslationData(string key, TranslationManager translationManager)
        {
            this._key = key;
            _translationManager = translationManager;
            if (translationManager != null)
            {
                LanguageChangedEventManager.AddListener(translationManager, this);
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="TranslationData"/> is reclaimed by garbage collection.
        /// </summary>
        ~TranslationData()
        {
            if (_translationManager != null)
            {
                LanguageChangedEventManager.RemoveListener(_translationManager, this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The value bound to, this will contain the translated text
        /// </summary>
        public string Value
        {
            get
            {
                if (_translationManager != null)
                {
                    var translationInfo = _translationManager.GetInfo(_key);
                    switch (translationInfo)
                    {
                        case TranslationInfo.CanTranslate:
                            var translate = _translationManager.Translate(_key);
                            return translate;
                        case TranslationInfo.MissingKey:
                            return string.Format(Properties.Resources.MissingKeyFormat, this._key);
                        case TranslationInfo.NoLanguages:
                            return string.Format(Properties.Resources.NoLanguagesFormat, this._key);
                        case TranslationInfo.NoResources:
                            return string.Format(Properties.Resources.MissingResourcesFormat, this._key);
                        case TranslationInfo.NoProvider:
                            return string.Format(Properties.Resources.NullManagerFormat, this._key);
                        case TranslationInfo.NoTranslation:
                            return string.Format(Properties.Resources.MissingTranslationFormat, this._key);
                        default:
                            break;
                    }
                }
                return string.Format(Properties.Resources.NullManagerFormat, this._key);
            }
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                this.OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}