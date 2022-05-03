<template>
    <v-dialog persistent v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <v-card>
            <v-toolbar dark color="primary">
                <v-btn icon dark @click="Close()" :loading="save">
                    <v-icon>mdi-close</v-icon>
                </v-btn>
                <v-toolbar-title>Cadastro de Filmes</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-toolbar-items>
                    <v-btn dark text @click="Salvar()" :disabled="!valid" :loading="save">Salvar</v-btn>
                </v-toolbar-items>
                <template v-slot:extension>
                    <v-tabs v-model="tabActive" color="white">
                        <v-tab>Geral</v-tab>
                        <v-tab>Diretores</v-tab>
                    </v-tabs>
                </template>
            </v-toolbar>
            <v-tabs-items v-model="tabActive">
                <v-tab-item>
                    <v-form ref="formFilme" v-model="valid" lazy-validation>
                        <v-card-text class="mt-3">
                            <v-row>
                                <v-col cols="12" sm="8" md="8">
                                    <v-text-field color="white" v-model="item.titulo" label="Titulo" :rules="fieldRules" :counter="45" maxLenght="45" outlined dense/>
                                </v-col>
                                <v-col cols="12" sm="4" md="4">                    
                                    <v-text-field type="date" color="white" v-model="item.dataLancamento" label="Data Lançamento" outlined dense/>
                                </v-col>
                                <v-col cols="12" sm="6" md="6">
                                    <v-text-field color="white" v-model="item.genero" label="Gênero" outlined dense/>
                                </v-col>
                                <v-col cols="12" md="6" lg="6" class="mb-5" >
                                    <div v-if="!item.poster">
                                        <v-file-input color="white" prepend-icon="mdi-camera" accept="image/png, image/jpeg" show-size label="Pôster" v-model="poster" outlined dense @change="LoadPoster()"/>
                                    </div>
                                    <div class="text-right" v-else style="justify-content: flex-start ; display: flex">
                                        <v-img contain :src="`data:image/jpeg;base64,${item.poster}`" style="max-height: 200px ; max-width: 280px; background-color: white; border-radius: 10px">
                                        <v-btn color="error" icon @click="RemovePoster()"><v-icon>mdi-close</v-icon></v-btn>    
                                        </v-img>
                                    </div>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-form>
                </v-tab-item>
                <v-tab-item>
                    <v-form ref="formDiretores" v-model="validDiretores" lazy-validation>
                        <v-card-text class="mt-3">
                            <v-row>
                                <v-col cols="12" sm="12" md="12">
                                    <v-data-table :headers="headerDiretor" :items="item.diretores" :items-per-page="-1" hide-default-footer class="elevation-1">
                                    </v-data-table>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-form>
                </v-tab-item>
            </v-tabs-items>
        </v-card>
    </v-dialog>
</template>
<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import jiff from 'jiff';
import { PageBase } from "@/core/models/shared";
import { Filme } from "@/core/models/all";
import { FilmeService } from "@/core/services/geral";
import { ArquivoService } from "@/core/services/shared";

@Component
export default class CadastroFilme extends PageBase{
    @Prop() private item!:Filme;
    @Prop() private value!: string;

    save : boolean = false;
    itemOriginal!:Filme;
    valid = true;
    validDiretores = true;
    dialog =false;
    fieldRules: any[] = [(v: any) => !!v || "Campo obrigatório"];
    service = new FilmeService();
    poster: any = null;
    tabActive: any = null;

    $refs!:{
        formFilme: HTMLFormElement
        formDiretores: HTMLFormElement
    }

    headerDiretor: any[] = [
        { text: '', value: 'actions', sortable: false },
        { text: "Nome", value: "nome" },
    ];

    @Watch('item')
    Item(){
        if (this.$refs.formFilme) {
        this.$refs.formFilme.resetValidation();
        }
    }

    @Watch('dialog')
    Dialog(){
        if(!this.dialog){
            this.$emit("fechou");
        }
    }
    
    @Watch('value')
    Value (){
        this.dialog = this.value ? true:false;

        if (this.dialog){
            this.itemOriginal = jiff.clone(this.item);
        }
    }

    beforeUpdate() {
        if(!this.dialog){
            this.$emit("fechou")
        }   
    }

    Salvar(){
        if(this.$refs.formFilme.validate()){
            this.save = true;

            let patchModel = jiff.diff(this.itemOriginal,this.item,false);

            (this.item.id > 0 ? this.service.Salvar(patchModel,this.item.id):this.service.Salvar(this.item))
            .then(
                res => {
                    this.$swal("Aviso","Operação realizada com sucesso!",res.status == 201 || res.status == 200? "success" : "warning");
                    this.$emit("salvou");
                    this.Close();
                }
            ).finally(() => {
                this.save =false;
            })
        }
    }

    async LoadPoster(){
        if (!this.poster)
        return;

        const arquivoService = new ArquivoService();
        const dados = await arquivoService.Ler(this.poster);
        this.item.poster = dados.replace(/^[^,]*,/, "");
    }

    RemovePoster(){
        this.item.poster = null;
        this.poster = null;
    }

    Close(){
        this.dialog = false;
    }

}
</script>