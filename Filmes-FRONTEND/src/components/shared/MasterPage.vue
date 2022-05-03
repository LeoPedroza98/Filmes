<template>
    <v-container fluid class="pa-0">
        <v-app-bar color="primary" dark flat prominent extended max-height="175px">
            <v-app-bar-nav-icon @click.stop="drawer = !drawer" color="white" />
            <v-spacer/>
            
            <v-menu offset-y transition="slide-y-transition">
                <template v-slot:activator="{ on }">
                    <v-btn v-on="on" icon class="mr-2 mt-2">
                        <v-avatar style="background-color: var(--v-primary-lighten2) !important;">
                            <img v-if="app.usuarioFoto" :src="`data:image/jpeg;base64,${app.usuarioFoto}`">
                            <v-icon v-else size="32" dark>mdi-account-circle</v-icon>
                        </v-avatar>
                    </v-btn>
                </template>
                <v-list dense>
                    <v-list-item>
                        <v-list-item-title>{{app.usuarioNome ? app.usuarioNome : ''}}</v-list-item-title>
                    </v-list-item>
                    <v-divider/>
                    <v-list-item @click="AlterarSenha()">
                        <v-list-item-icon><v-icon>mdi-lock-reset</v-icon></v-list-item-icon>
                        <v-list-item-title>Alterar Senha</v-list-item-title>
                    </v-list-item>
                    <v-list-item @click="Logout()">
                        <v-list-item-icon><v-icon>mdi-logout</v-icon></v-list-item-icon>
                        <v-list-item-title>Sair</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>

        </v-app-bar> 

        <v-card class="mx-md-5" style="margin-top: -95px ; border-radius: 10px">                
            
            <v-toolbar flat class="grey--text" style="background: var(--v-secondary-base) !important">
                <v-toolbar-title>
                    <div v-if="icone" class="d-flex mb-1" style="display: flex ; align-items:center">
                        <div class="d-flex pt-3">
                            <v-avatar color="white" size="40" class="elevation-2">
                                <v-icon size="25" color="primary">{{ icone }}</v-icon>
                            </v-avatar>
                        </div>
                        <div class="pl-3 pt-3">
                            <h4 class="font-weight-medium">{{ titulo }}</h4>
                            <div class="caption">{{ subTitulo }}</div>
                        </div>
                    </div>
                </v-toolbar-title>                    
                <v-spacer/>                    
                <slot name="menu"/>
            </v-toolbar>        
            <v-divider/>        
            <v-card-text style="background: var(--v-secondary-base) !important">
                <slot/>
            </v-card-text>
        </v-card>


        <v-navigation-drawer v-model="drawer" fixed temporary width="300">
            <template v-slot:prepend>
                <v-list nav dense>                    
                    <v-subheader>MENU</v-subheader>

                    <v-list-item-group active-class="blue-grey--text text--accent-4">
                        <v-list-item to="/">
                            <v-list-item-icon> <v-icon>mdi-home</v-icon> </v-list-item-icon>
                            <v-list-item-content> <v-list-item-title>Principal</v-list-item-title> </v-list-item-content>
                        </v-list-item>
                    </v-list-item-group>

                    <!-- CADASTROS -->
                    <v-list-group prepend-icon="mdi-text-box-multiple-outline">
                        <template v-slot:activator>
                            <v-list-item-title>Cadastro</v-list-item-title>
                        </template>
                        <div v-for="(item, index) in cadastroMenuButtons" :key="index">
                            <v-list-item :to="item.path" :disabled="item.disabled">
                                <v-list-item-title>{{item.name}}</v-list-item-title>
                                <v-list-item-icon> <v-icon>{{item.icon}}</v-icon> </v-list-item-icon>
                            </v-list-item>
                        </div>
                    </v-list-group>

                </v-list>
            </template>
            <template v-slot:append>
                <div class="pa-2">
                    <v-btn block color="error" @click="Logout()">SAIR</v-btn>
                </div>
            </template>
        </v-navigation-drawer>

        <v-footer color="primary" app>
            <div class="flex-grow-1"></div>
        </v-footer>

        <alterar-senha v-model="dialogAlterarSenha" @fechou="dialogAlterarSenha = false"/>

    </v-container>
</template>

<script lang="ts">
import { EnumPerfilUsuario, PageBase } from "@/core/models/shared";
import { Component, Prop, Watch, Vue } from "vue-property-decorator";

@Component
export default class MasterPage extends PageBase {
    @Prop() private icone!: string;
    @Prop() private titulo!: string;
    @Prop() private subTitulo!: string;

    drawer:boolean = false;
    active:boolean = false;
    dialogAlterarSenha: boolean = false;

    enumMaster = EnumPerfilUsuario.Master;

    cadastroMenuButtons: any[] = [
        {name:'Diretor',                icon:'mdi-card-account-details',               path:'/all/diretor',                              disabled: false},
        {name:'Filmes',                 icon:'mdi-filmstrip',                         path:'/all/filme',                                 disabled: false},
        {name:'Filmes e Diretores',     icon:'mdi-movie-settings',                     path:'/all/filmeDiretor',                         disabled: false},
        {name:'Usuarios',               icon:'mdi-account',                            path:'/all/usuario',                              disabled: false},
    ];

    mounted() {
        this.$vuetify.theme.dark = true;
    }

    GetDiaSemana(dia: number, lingua: string){
        const diaSemanaString = new Date(Date.UTC(1, 0, dia)).toLocaleDateString(lingua, {weekday: 'long'});
        return diaSemanaString;
    }

    MudarTema(){
        alert('Deveria mudar o tema');
    };   
    
    AlterarSenha(){
        this.dialogAlterarSenha = true;
    };

    Logout() {
        localStorage.clear();
        this.$router.push({name: 'Login'});
    };
}
</script>

<style >

    #app{
        background-color: var(--v-background-base);
    }

    .drawerSize{
        size: 80%;
    }

    .col-12 {
        padding-top: 5px !important;
        padding-bottom: 0px !important;
    }

    .buscaRapida{
        margin-top: 25px !important; 
        margin-right: 20px;
    }

    .itemExcluido{
        text-decoration: line-through; 
        text-decoration-color: red;
        opacity: 0.5;
    }

    .inputUpperCase{
        text-transform: uppercase
    }

    .v-navigation-drawer{
        background-color: var(--v-background-base) !important;
    }

</style>

