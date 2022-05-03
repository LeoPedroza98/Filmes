<template>
    <v-dialog v-model="dialog" max-width="600" persistent>

        <v-card>
            <v-toolbar dark color="primary">
                <v-toolbar-title class="headline primary white--text"><v-icon color="white" class="pr-2">mdi-form-textbox-password</v-icon> Alterar Senha </v-toolbar-title>
            </v-toolbar>

            <v-form @submit.prevent="Salvar()" ref="form" v-model="validDialog" lazy-validation>

                <v-card-text class="mt-4">

                    <v-row>
                        <v-col cols="12" sm="12" md="12">
                            <v-text-field color="white" :rules="fieldRules" v-model="senhaAntiga" :type="showPassword1 ? 'text' : 'password'" label="Senha Atual" prepend-icon="mdi-form-textbox-password" :append-icon="showPassword1 ? 'mdi-eye' : 'mdi-eye-off'" @click:append="showPassword1 = !showPassword1"/>
                        </v-col>
                        <v-col cols="12" sm="12" md="12">
                            <v-text-field color="white" :rules="fieldRules" v-model="senhaNova" :type="showPassword2 ? 'text' : 'password'" label="Senha Nova" prepend-icon="mdi-form-textbox-password" :append-icon="showPassword2 ? 'mdi-eye' : 'mdi-eye-off'" @click:append="showPassword2 = !showPassword2"/>
                        </v-col>
                        <v-col cols="12" sm="12" md="12">
                            <v-text-field color="white" :rules="[(senhaNova === repetirSenha) || 'As senhas devem ser iguais!']" v-model="repetirSenha" :type="showPassword3 ? 'text' : 'password'" label="Repetir Senha" prepend-icon="mdi-form-textbox-password" :append-icon="showPassword3 ? 'mdi-eye' : 'mdi-eye-off'" @click:append="showPassword3 = !showPassword3"/>
                        </v-col>
                    </v-row>

                </v-card-text>

                <v-card-actions class="teste">
                    <v-spacer/>
                    <v-btn text @click="dialog = false">Fechar</v-btn>
                    <v-btn text type="submit" :disabled="!validDialog" >Salvar</v-btn>
                </v-card-actions>

            </v-form>

        </v-card>

    </v-dialog>
</template>

<script lang="ts">
import { PageBase } from "@/core/models/shared";
import { UsuarioService } from "@/core/services/geral";
// import { UsuarioService } from "@/core/services/geral";
import { Component, Prop, Watch } from "vue-property-decorator";

@Component
export default class AlterarSenha extends PageBase{
    @Prop() private value!: string;

    validDialog: boolean = true;
    dialog: boolean = false;

    senhaAntiga: string = '';
    senhaNova: string = '';
    repetirSenha: string = '';

    showPassword1: boolean = false;
    showPassword2: boolean = false;
    showPassword3: boolean = false;

    fieldRules: any[] = [(v: any) => !!v || "Campo obrigatório"];

    $refs!:{
        form: HTMLFormElement
    }

    usuarioService = new UsuarioService();

    @Watch("value")
    Value(){
        this.dialog = this.value ? true : false;
        if(this.dialog){
            if (this.$refs.form) {
                this.$refs.form.resetValidation();
            }
        }
    }

    @Watch("dialog")
    Dialog() {
        if (!this.dialog) {
            this.$emit("fechou");
        }
    }

    beforeUpdate() {
        if(!this.dialog){
            this.$emit("fechou");
        }
    }

    Salvar(){

        this.usuarioService.AlterarSenha(this.app.usuarioId, this.senhaAntiga, this.senhaNova)
            .then(
                res => {
                    this.sessionApp.dados.primeiroAcesso = false;
                    localStorage.setItem('sessionApp', JSON.stringify(this.sessionApp));              

                    this.Close();
                    this.$swal("Aviso", res.data, res.status == 200 ? "success" : "warning");
                    this.senhaNova = '';
                    this.senhaAntiga = '';
                    this.repetirSenha = '';
                },
                err => this.$swal('Aviso', err.response.data, err.response.status === 400 ? 'warning' : 'error')
            );
        
    };

    Close() {
        this.dialog = false;
    }
}
</script>