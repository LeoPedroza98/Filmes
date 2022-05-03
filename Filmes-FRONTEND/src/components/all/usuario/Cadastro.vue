<template>
    <v-dialog persistent v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <v-card>
            <v-toolbar dark color="primary">
                <v-btn icon dark @click="Close()">
                    <v-icon>mdi-close</v-icon>
                </v-btn>
                <v-toolbar-title>Cadastro de Usuário</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-toolbar-items>
                    <v-btn dark text @click="Salvar()" :disabled="!valid">Salvar</v-btn>
                </v-toolbar-items>
            </v-toolbar>
            <v-form ref="formUsuario" v-model="valid" lazy-validation>
                <v-card-text class="mt-5">
                    <v-row style="display:flex; justify-content: center">
                        <v-col class="mb-4" cols="12" sm="12" md="12" lg="3" align-self="center" style="display: flex ; justify-content: center">
                            <v-card height="250" width="300">
                                <v-row>
                                    <v-col class="mx-2">
                                        <v-skeleton-loader v-bind="attrs = {boilerplate: true}" max-width="300" max-height="150" type="image" v-if="!item.foto"/>
                                        <v-img v-if="item.foto" contain :src="`data:image/jpeg;base64,${item.foto}`" style="max-height: 300px"></v-img>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col class="ma-2">
                                        <div>
                                        <div v-if="!item.foto" >
                                            <v-file-input color="white" prepend-icon="mdi-camera" accept="image/png, image/jpeg" show-size label="Foto" v-model="foto" outlined dense @change="LoadImage()"/>
                                        </div>
                                        <div class="text-right" v-else>
                                            <v-btn icon @click="RemoveImage()"><v-icon>mdi-close</v-icon></v-btn>                    
                                        </div>
                                        </div>
                                    </v-col>
                                </v-row>
                            </v-card>
                        </v-col>
                        <v-col cols="12" sm="12" md="12" lg="9">
                            <v-row>
                                <v-col cols="12" md="4">
                                    <v-text-field color="white" v-model="item.login" label="Login" :rules="fieldRules" :counter="45" max-lenght="45" :disabled="item.id > 0" dense outlined/>
                                </v-col>
                                <v-col cols="12" md="4">
                                    <v-text-field color="white" type="password" :disabled="item.id != 0" v-model="item.senha" label="Senha" :rules=" item.id == 0 ? fieldRules : []" dense outlined/>
                                </v-col>
                                <v-col cols="12" md="3">
                                    <v-select color="white" :items="perfis" item-value="id" item-text="nome" v-model="item.perfilId" label="Perfil" :rules="fieldRules" dense outlined/>
                                </v-col>
                                <v-col cols="12" md="2" align-self="center">
                                    <v-switch color="white" v-model="item.ativo" dense label="Ativo" style="margin-top: -10px"/>
                                </v-col>
                            </v-row>
                        </v-col>
                    </v-row>
                </v-card-text>
            </v-form>
        </v-card>
    </v-dialog>
</template>
<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import jiff from 'jiff';
import { EnumPerfilUsuario, PageBase } from "@/core/models/shared";
import { PerfilUsuario, Usuario } from "@/core/models/all";
import { PerfilUsuarioService, UsuarioService } from "@/core/services/geral";

@Component
export default class CadastroUsuario extends PageBase {
    @Prop() private item!:Usuario;
    @Prop() private value!: string;

    save : boolean = false;
    itemOriginal!:Usuario;
    service = new UsuarioService();
    valid = true;
    dialog = false;
    fieldRules: any[] = [(v: any) => !!v || "Campo obrigatório"];
    perfis: PerfilUsuario[] = [];
    foto: any = null;
    usuarioId:number = 0;

    $refs!: {
        formUsuario: HTMLFormElement
    }
    enumMaster = EnumPerfilUsuario.Master;

    @Watch('item')
    Item() {
        if (this.$refs.formUsuario) {
        this.$refs.formUsuario.resetValidation();
        }
    }

    @Watch('value')
    Value() {
        this.dialog = this.value ? true : false;

        if (this.dialog){
            this.itemOriginal = jiff.clone(this.item);
        }
    }
    
    @Watch('dialog')
    Dialog() {
        if (!this.dialog) {
            this.$emit("fechou");
        }
    }

    
    beforeUpdate(){
        if (!this.dialog){
            this.$emit('fechou');
        }
    }

    RemoveImage(){
        this.item.foto = null;
        this.foto = null;
    }

    mounted(){
        const perfilservice = new PerfilUsuarioService();
        perfilservice.ListarTudo().then(
        res => {
            this.perfis = res.data.items;
        },
        err => {
            this.$swal('Aviso', err.message, 'error')}
        );
    }


    Salvar() {
        if (this.$refs.formUsuario.validate()) {

        let pacthModel = jiff.diff(this.itemOriginal, this.item, false);

        (this.item.id > 0 ? this.service.Salvar(pacthModel, this.item.id) : this.service.Salvar(this.item))
        .then(
            res => {
                this.$swal("Aviso", "Operação realizada com sucesso!", res.status == 201 || res.status == 200 ? "success" : "warning");
                this.$emit("salvou");
                this.Close();
            },
            err => {
                this.$swal("Aviso", err.response.status == 403 ? "Você não tem permissão para essa funcionalidade!" : err.response.data, err.response.status >= 500 ? "error" : "warning" );
            }
        );
        }
    }

    Close() {
        this.dialog = false;
        this.usuarioId = 0;
    }
}
</script>
