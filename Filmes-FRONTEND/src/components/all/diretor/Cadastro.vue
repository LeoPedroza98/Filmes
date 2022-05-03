<template>
    <v-dialog persistent v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <v-card>
            <v-toolbar dark color="primary">
                <v-btn icon dark @click="Close()" :loading="save">
                    <v-icon>mdi-close</v-icon>
                </v-btn>
                <v-toolbar-title>Cadastro de Diretores</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-toolbar-items>
                    <v-btn dark text @click="Salvar()" :disabled="!valid" :loading="save">Salvar</v-btn>
                </v-toolbar-items>
                <template v-slot:extension>
                    <v-tabs v-model="tabActive" color="white">
                        <v-tab>Geral</v-tab>
                        <v-tab>Filmes</v-tab>
                    </v-tabs>
                </template>
            </v-toolbar>
            <v-tabs-items v-model="tabActive">
                <v-tab-item>
                    <v-form ref="formPagamento" v-model="valid" lazy-validation>
                        <v-card-text class="mt-3">
                            <v-row>
                                <v-col cols="12" sm="6" md="6">
                                    <v-text-field color="white" v-model="item.nome" label="Nome" :rules="fieldRules" :counter="45" maxLenght="45" outlined dense/>
                                </v-col>
                                <v-col cols="12" sm="6" md="6">
                                    <v-text-field color="white" v-model="item.nacionalidade" label="Nacionalidade" outlined dense/>
                                </v-col>
                                <v-col cols="12" sm="6" md="6">                    
                                    <v-text-field type="date" color="white" v-model="item.dataNascimento" label="Data Nascimento" outlined dense/>
                                </v-col>
                                <v-col cols="12" sm="6" md="6">
                                    <v-select color="white" v-model="item.generoId" label="Gênero" :rules="fieldRules" item-value="id" item-text="nome" :items="generos" outlined dense />
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-form>
                </v-tab-item>
                <v-tab-item>
                    <v-form ref="formFilme" v-model="validFilme" lazy-validation>
                        <v-card-text class="mt-3">
                            <v-row>
                                <v-col cols="12" sm="12" md="12">
                                    <v-data-table :headers="headerFilmes" :items="item.filmes" :items-per-page="-1" hide-default-footer class="elevation-1">
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
import { Diretor, Filme, Genero } from "@/core/models/all";
import { DiretorService, FilmeService, GeneroService } from "@/core/services/geral";

@Component
export default class CadastroDiretor extends PageBase{
    @Prop() private item!:Diretor;
    @Prop() private value!: string;

    save : boolean = false;
    itemOriginal!:Diretor;
    valid = true;
    validFilme = true;
    dialog =false;
    fieldRules: any[] = [(v: any) => !!v || "Campo obrigatório"];
    service = new DiretorService();
    tabActive: any = null;


    generoId: number = 0;
    generoService = new GeneroService();
    isGeneroLoading:boolean=false;
    onSearchGenero: any = null;
    generos:Genero []=[];

    $refs!:{
        formDiretor: HTMLFormElement
        formFilme: HTMLFormElement
    }

    headerFilmes: any[] = [
        { text: '', value: 'actions', sortable: false },
        { text: "Titulo", value: "filmes.titulo" },
    ];

    @Watch('item')
    Item(){
        if (this.$refs.formDiretor) {
        this.$refs.formDiretor.resetValidation();
        }
    }

    @Watch('onSearchGenero')
    SearchGenero (val:string){
        if(this.item.generoId) return;
        if(this.isGeneroLoading) return;
        if (!val)return;
        this.isGeneroLoading = true
        this.generoService.AutoComplete(val)
        .then(
            res => {
                this.generos = res.data;
            },
            err => this.$swal('Aviso',err.response.data, 'error')
        ).finally(() => {
            this.isGeneroLoading = false;
        })
    }


    mounted(){
        this.generoService.ListarTudo().then(
        res => {
            this.generos = res.data.items;
        },
        err => {
            this.$swal('Aviso',err.message,'error')
        }
        )
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
        if(this.$refs.formDiretor.validate()){
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

    Close(){
        this.dialog = false;
    }

}
</script>