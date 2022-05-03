<template>
  <v-app class="login">
    <v-row style="display: flex; justify-content: space-between; align-items: center; margin-right: 150px; margin-left: 150px">
      
      <v-col cols="12" sm="12" md="6" lg="4" style="display: flex ; justify-content: center ; align-items: center">
        <img class="logo" src="">
      </v-col>

      <v-col cols="12" sm="12" md="6" lg="6">
        
        <v-form @submit.prevent="Autenticar()" v-if="!resetarSenha">              
          <v-card class="elevation-1" min-height="230px">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title><v-icon>mdi-lock</v-icon> Controle de Acesso</v-toolbar-title>
            </v-toolbar>
            <v-card-text style="display: flex ; justify-content: center ; align-items: center">
              <v-row>
                <v-col cols="12" sm="12" md="12">
                  <v-text-field color="white" label="Login" prepend-icon="mdi-account" v-model="acesso.login"/>
                </v-col>
                <v-col cols="12" sm="12" md="12">
                  <v-text-field color="white" :type="showPassword ? 'text' : 'password'" label="Senha" v-model="acesso.senha" prepend-icon="mdi-form-textbox-password" :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'" @click:append="showPassword = !showPassword"/>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <a style="text-decoration: underline;" @click="resetarSenha = true">Esqueceu a senha?</a>
              <div class="flex-grow-1"></div>
              <v-btn type="submit" color="primary" :loading="loading">Entrar</v-btn>
            </v-card-actions>
          </v-card>
        </v-form>

        <v-form @submit.prevent="EnviarEmail()" v-else>
          <v-card class="elevation-1">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title><v-icon>mdi-lock</v-icon> Redefinição de senha</v-toolbar-title>
            </v-toolbar>
            <v-card-text style="min-height: 159px ; display: flex ; justify-content: center ; align-items: center">
              <v-row>
                <v-col cols="12" sm="12" md="12">
                  <v-text-field color="white" label="Login" prepend-icon="mdi-account" v-model="acesso.login"/>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <v-btn color="secondary" :loading="loadingReset" @click="resetarSenha = false">Voltar</v-btn>
              <div class="flex-grow-1"></div>
              <v-btn type="submit" color="primary" :loading="loadingReset">Recuperar Senha</v-btn>
            </v-card-actions>
          </v-card>
        </v-form>

      </v-col>

    </v-row>
  </v-app>
</template>

<script lang="ts">
import { AutenticadorService } from '@/core/services/shared/AutenticadorService';
import { Vue, Component, Watch, Prop } from 'vue-property-decorator';

@Component
export default class Login extends Vue {
  @Prop() private value!: boolean;

  logo: string = '';
  hidePassword: boolean = true;
  loading: boolean = false;
  loadingReset: boolean = false;
  showPassword: boolean = false;
  acesso: {login: string, senha: string} = {
    login: '',
    senha: ''
  };
  fieldRules: any[] = [
    (v: any) => !!v || 'Campo obrigatório',
  ]
  service = new AutenticadorService();
  resetarSenha: boolean = false;

  Autenticar(){
    this.loading = true;
    localStorage.clear();

    this.service.AutenticarUsuario(this.acesso).then(
      res => {
        localStorage.setItem('sessionApp', JSON.stringify(res.data));                  
        this.$swal('Aviso', res.data.message, res.status == 200 ? 'success' : 'warning');
        this.$router.push({ name: 'Home' });
      }, 
      err => {
        localStorage.clear();
        this.$swal('Aviso', err.response.data, err.response.status === 400 ? 'warning' : 'error')
      }
    ).finally(() => this.loading = false);
  }
}
</script>

<style scoped>

  .login{
    background-color: var(--v-background-base) !important;
  }
  .container{
    display: flex;
    align-content: center !important;
    justify-content: center !important;
    height: 100vh;
  }
  .logo{
    max-height: 35vh;
    max-width: 30vw;
    left: 0;
    top: 0;
  }
  .formContainer{
    display: flex;
    justify-content: center;
    align-content: center;
    margin: 50px 0px 0px 50px;
  }
  .login-extra {
    position: absolute;
    bottom: 0;
    left: 0;
    padding: 20px;
  }

</style>