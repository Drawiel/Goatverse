﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Goatverse.GoatverseService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserData", Namespace="http://schemas.datacontract.org/2004/07/GoatverseService")]
    [System.SerializableAttribute()]
    public partial class UserData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdUserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdUser {
            get {
                return this.IdUserField;
            }
            set {
                if ((object.ReferenceEquals(this.IdUserField, value) != true)) {
                    this.IdUserField = value;
                    this.RaisePropertyChanged("IdUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageData", Namespace="http://schemas.datacontract.org/2004/07/GoatverseService")]
    [System.SerializableAttribute()]
    public partial class MessageData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdUserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LobbyCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdUser {
            get {
                return this.IdUserField;
            }
            set {
                if ((object.ReferenceEquals(this.IdUserField, value) != true)) {
                    this.IdUserField = value;
                    this.RaisePropertyChanged("IdUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LobbyCode {
            get {
                return this.LobbyCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.LobbyCodeField, value) != true)) {
                    this.LobbyCodeField = value;
                    this.RaisePropertyChanged("LobbyCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProfileData", Namespace="http://schemas.datacontract.org/2004/07/GoatverseService")]
    [System.SerializableAttribute()]
    public partial class ProfileData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdProfileField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdUserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ImageIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MatchesWonField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ProfileLevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalPointsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdProfile {
            get {
                return this.IdProfileField;
            }
            set {
                if ((this.IdProfileField.Equals(value) != true)) {
                    this.IdProfileField = value;
                    this.RaisePropertyChanged("IdProfile");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdUser {
            get {
                return this.IdUserField;
            }
            set {
                if ((this.IdUserField.Equals(value) != true)) {
                    this.IdUserField = value;
                    this.RaisePropertyChanged("IdUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ImageId {
            get {
                return this.ImageIdField;
            }
            set {
                if ((this.ImageIdField.Equals(value) != true)) {
                    this.ImageIdField = value;
                    this.RaisePropertyChanged("ImageId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MatchesWon {
            get {
                return this.MatchesWonField;
            }
            set {
                if ((this.MatchesWonField.Equals(value) != true)) {
                    this.MatchesWonField = value;
                    this.RaisePropertyChanged("MatchesWon");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProfileLevel {
            get {
                return this.ProfileLevelField;
            }
            set {
                if ((this.ProfileLevelField.Equals(value) != true)) {
                    this.ProfileLevelField = value;
                    this.RaisePropertyChanged("ProfileLevel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalPoints {
            get {
                return this.TotalPointsField;
            }
            set {
                if ((this.TotalPointsField.Equals(value) != true)) {
                    this.TotalPointsField = value;
                    this.RaisePropertyChanged("TotalPoints");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GoatverseService.IUsersManager")]
    public interface IUsersManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceTryLogin", ReplyAction="http://tempuri.org/IUsersManager/ServiceTryLoginResponse")]
        bool ServiceTryLogin(Goatverse.GoatverseService.UserData userData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceTryLogin", ReplyAction="http://tempuri.org/IUsersManager/ServiceTryLoginResponse")]
        System.Threading.Tasks.Task<bool> ServiceTryLoginAsync(Goatverse.GoatverseService.UserData userData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceTrySignIn", ReplyAction="http://tempuri.org/IUsersManager/ServiceTrySignInResponse")]
        bool ServiceTrySignIn(Goatverse.GoatverseService.UserData userData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceTrySignIn", ReplyAction="http://tempuri.org/IUsersManager/ServiceTrySignInResponse")]
        System.Threading.Tasks.Task<bool> ServiceTrySignInAsync(Goatverse.GoatverseService.UserData userData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceUserExistsByUsername", ReplyAction="http://tempuri.org/IUsersManager/ServiceUserExistsByUsernameResponse")]
        bool ServiceUserExistsByUsername(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceUserExistsByUsername", ReplyAction="http://tempuri.org/IUsersManager/ServiceUserExistsByUsernameResponse")]
        System.Threading.Tasks.Task<bool> ServiceUserExistsByUsernameAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceVerifyPassword", ReplyAction="http://tempuri.org/IUsersManager/ServiceVerifyPasswordResponse")]
        bool ServiceVerifyPassword(string password, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServiceVerifyPassword", ReplyAction="http://tempuri.org/IUsersManager/ServiceVerifyPasswordResponse")]
        System.Threading.Tasks.Task<bool> ServiceVerifyPasswordAsync(string password, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServicePasswordChanged", ReplyAction="http://tempuri.org/IUsersManager/ServicePasswordChangedResponse")]
        bool ServicePasswordChanged(Goatverse.GoatverseService.UserData userData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/ServicePasswordChanged", ReplyAction="http://tempuri.org/IUsersManager/ServicePasswordChangedResponse")]
        System.Threading.Tasks.Task<bool> ServicePasswordChangedAsync(Goatverse.GoatverseService.UserData userData);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersManagerChannel : Goatverse.GoatverseService.IUsersManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsersManagerClient : System.ServiceModel.ClientBase<Goatverse.GoatverseService.IUsersManager>, Goatverse.GoatverseService.IUsersManager {
        
        public UsersManagerClient() {
        }
        
        public UsersManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsersManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ServiceTryLogin(Goatverse.GoatverseService.UserData userData) {
            return base.Channel.ServiceTryLogin(userData);
        }
        
        public System.Threading.Tasks.Task<bool> ServiceTryLoginAsync(Goatverse.GoatverseService.UserData userData) {
            return base.Channel.ServiceTryLoginAsync(userData);
        }
        
        public bool ServiceTrySignIn(Goatverse.GoatverseService.UserData userData) {
            return base.Channel.ServiceTrySignIn(userData);
        }
        
        public System.Threading.Tasks.Task<bool> ServiceTrySignInAsync(Goatverse.GoatverseService.UserData userData) {
            return base.Channel.ServiceTrySignInAsync(userData);
        }
        
        public bool ServiceUserExistsByUsername(string userName) {
            return base.Channel.ServiceUserExistsByUsername(userName);
        }
        
        public System.Threading.Tasks.Task<bool> ServiceUserExistsByUsernameAsync(string userName) {
            return base.Channel.ServiceUserExistsByUsernameAsync(userName);
        }
        
        public bool ServiceVerifyPassword(string password, string username) {
            return base.Channel.ServiceVerifyPassword(password, username);
        }
        
        public System.Threading.Tasks.Task<bool> ServiceVerifyPasswordAsync(string password, string username) {
            return base.Channel.ServiceVerifyPasswordAsync(password, username);
        }
        
        public bool ServicePasswordChanged(Goatverse.GoatverseService.UserData userData) {
            return base.Channel.ServicePasswordChanged(userData);
        }
        
        public System.Threading.Tasks.Task<bool> ServicePasswordChangedAsync(Goatverse.GoatverseService.UserData userData) {
            return base.Channel.ServicePasswordChangedAsync(userData);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GoatverseService.ILobbyManager", CallbackContract=typeof(Goatverse.GoatverseService.ILobbyManagerCallback))]
    public interface ILobbyManager {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/ServiceSendMessageToLobby")]
        void ServiceSendMessageToLobby(Goatverse.GoatverseService.MessageData messageData);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/ServiceSendMessageToLobby")]
        System.Threading.Tasks.Task ServiceSendMessageToLobbyAsync(Goatverse.GoatverseService.MessageData messageData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceConnectToLobby", ReplyAction="http://tempuri.org/ILobbyManager/ServiceConnectToLobbyResponse")]
        bool ServiceConnectToLobby(string username, string lobbyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceConnectToLobby", ReplyAction="http://tempuri.org/ILobbyManager/ServiceConnectToLobbyResponse")]
        System.Threading.Tasks.Task<bool> ServiceConnectToLobbyAsync(string username, string lobbyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceDisconnectFromLobby", ReplyAction="http://tempuri.org/ILobbyManager/ServiceDisconnectFromLobbyResponse")]
        bool ServiceDisconnectFromLobby(string username, string lobbyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceDisconnectFromLobby", ReplyAction="http://tempuri.org/ILobbyManager/ServiceDisconnectFromLobbyResponse")]
        System.Threading.Tasks.Task<bool> ServiceDisconnectFromLobbyAsync(string username, string lobbyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceCountPlayersInLobby", ReplyAction="http://tempuri.org/ILobbyManager/ServiceCountPlayersInLobbyResponse")]
        int ServiceCountPlayersInLobby(string lobbycode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceCountPlayersInLobby", ReplyAction="http://tempuri.org/ILobbyManager/ServiceCountPlayersInLobbyResponse")]
        System.Threading.Tasks.Task<int> ServiceCountPlayersInLobbyAsync(string lobbycode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyManagerCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/ServiceGetMessage")]
        void ServiceGetMessage(Goatverse.GoatverseService.MessageData messageData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceSuccessfulJoin", ReplyAction="http://tempuri.org/ILobbyManager/ServiceSuccessfulJoinResponse")]
        bool ServiceSuccessfulJoin();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/ServiceSucessfulLeave", ReplyAction="http://tempuri.org/ILobbyManager/ServiceSucessfulLeaveResponse")]
        bool ServiceSucessfulLeave();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyManagerChannel : Goatverse.GoatverseService.ILobbyManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyManagerClient : System.ServiceModel.DuplexClientBase<Goatverse.GoatverseService.ILobbyManager>, Goatverse.GoatverseService.ILobbyManager {
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ServiceSendMessageToLobby(Goatverse.GoatverseService.MessageData messageData) {
            base.Channel.ServiceSendMessageToLobby(messageData);
        }
        
        public System.Threading.Tasks.Task ServiceSendMessageToLobbyAsync(Goatverse.GoatverseService.MessageData messageData) {
            return base.Channel.ServiceSendMessageToLobbyAsync(messageData);
        }
        
        public bool ServiceConnectToLobby(string username, string lobbyCode) {
            return base.Channel.ServiceConnectToLobby(username, lobbyCode);
        }
        
        public System.Threading.Tasks.Task<bool> ServiceConnectToLobbyAsync(string username, string lobbyCode) {
            return base.Channel.ServiceConnectToLobbyAsync(username, lobbyCode);
        }
        
        public bool ServiceDisconnectFromLobby(string username, string lobbyCode) {
            return base.Channel.ServiceDisconnectFromLobby(username, lobbyCode);
        }
        
        public System.Threading.Tasks.Task<bool> ServiceDisconnectFromLobbyAsync(string username, string lobbyCode) {
            return base.Channel.ServiceDisconnectFromLobbyAsync(username, lobbyCode);
        }
        
        public int ServiceCountPlayersInLobby(string lobbycode) {
            return base.Channel.ServiceCountPlayersInLobby(lobbycode);
        }
        
        public System.Threading.Tasks.Task<int> ServiceCountPlayersInLobbyAsync(string lobbycode) {
            return base.Channel.ServiceCountPlayersInLobbyAsync(lobbycode);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GoatverseService.IProfilesManager")]
    public interface IProfilesManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProfilesManager/ServiceLoadProfileData", ReplyAction="http://tempuri.org/IProfilesManager/ServiceLoadProfileDataResponse")]
        Goatverse.GoatverseService.ProfileData ServiceLoadProfileData(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProfilesManager/ServiceLoadProfileData", ReplyAction="http://tempuri.org/IProfilesManager/ServiceLoadProfileDataResponse")]
        System.Threading.Tasks.Task<Goatverse.GoatverseService.ProfileData> ServiceLoadProfileDataAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProfilesManagerChannel : Goatverse.GoatverseService.IProfilesManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProfilesManagerClient : System.ServiceModel.ClientBase<Goatverse.GoatverseService.IProfilesManager>, Goatverse.GoatverseService.IProfilesManager {
        
        public ProfilesManagerClient() {
        }
        
        public ProfilesManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProfilesManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProfilesManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProfilesManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Goatverse.GoatverseService.ProfileData ServiceLoadProfileData(string userName) {
            return base.Channel.ServiceLoadProfileData(userName);
        }
        
        public System.Threading.Tasks.Task<Goatverse.GoatverseService.ProfileData> ServiceLoadProfileDataAsync(string userName) {
            return base.Channel.ServiceLoadProfileDataAsync(userName);
        }
    }
}
