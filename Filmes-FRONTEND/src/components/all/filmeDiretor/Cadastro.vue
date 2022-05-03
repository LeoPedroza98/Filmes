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
            </v-toolbar>
            <v-form ref="formFilmeDiretor" v-model="valid" lazy-validation>
                <v-card-text class="mt-3">
                    <v-row>
                        <v-col cols="12" sm="12" md="12">
                            <v-text-field v-if="item.filme" v-model="item.filme.nome" color="white" label="Filme" dense readonly outlined/>
                            <v-autocomplete v-else color="white" v-model="item.filmeId" :items="Filmes" :loading="isFilmeLoading" :search-input.sync="onSearchFilmes" :rules="fieldRules" clearable
                            item-text="nome" item-value="id" label="Filme" outlined dense/>
                        </v-col>
                        <v-col cols="12" sm="12" md="12">
                            <v-autocomplete v-if="!item.id > 0" color="white" v-model="item.diretorId" :items="diretores" :loading="isDiretorLoading" :search-input.sync="onSearchDiretor" :rules="fieldRules" clearable
                            item-text="nome" item-value="id" label="Diretor" outlined dense :disabled="item.id > 0"/>
                            <v-text-field v-else v-model="item.diretor.nome" color="white" label="Diretor" dense readonly outlined/>
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
import { PageBase } from "@/core/models/shared";
import { Diretor, Filme, FilmeDiretor } from "@/core/models/all";
import { DiretorService, FilmeDiretorService, FilmeService } from "@/core/services/geral";

@Component
export default class CadastroFilmeDiretor extends PageBase{
    @Prop() private item!:FilmeDiretor;
    @Prop() private value!: string;

    save : boolean = false;
    itemOriginal!:FilmeDiretor;
    valid = true;
    validDiretores = true;
    dialog =false;
    fieldRules: any[] = [(v: any) => !!v || "Campo obrigatório"];
    service = new FilmeDiretorService();
    poster: any = null;
    tabActive: any = null;

    FilmeService = new FilmeService();
    filmes: Filme[]=[];
    filmeService = new FilmeService();
    
    diretor: Diretor = new Diretor();
    diretores:any[]=[];
    diretorService = new DiretorService();

    isFilmeLoading:boolean = false;
    isDiretorLoading:boolean = false;

    onSearchFilme: any = null;
    onSearchDiretor:any = null;

    $refs!:{
        formFilmeDiretor: HTMLFormElement
    }

    @Watch('onSearchFilme')
    SearchFilmes (val:string){
        if(this.item.filmeId || this.isFilmeLoading || !val) 
        return;

        this.isFilmeLoading = true
        this.filmeService.AutoComplete(val).then(
        res =>{
            this.filmes = res.data;
        },
        err => this.$swal('Aviso',err.response.data,'error')
        ).finally(() =>{
        this.isFilmeLoading = false;
        })
    }

    @Watch('onSearchDiretor')
    SearchDiretores (val:string){
        if(this.item.diretorId || this.isDiretorLoading || !val) 
        return;

        this.isDiretorLoading = true
        this.filmeService.AutoComplete(val).then(
        res =>{
            this.diretores = res.data;
        },
        err => this.$swal('Aviso',err.response.data,'error')
        ).finally(() =>{
        this.isDiretorLoading = false;
        })
    }

    @Watch('item')
    Item(){
        if (this.$refs.formFilmeDiretor) {
        this.$refs.formFilmeDiretor.resetValidation();
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
        if(this.$refs.formFilmeDiretor.validate()){
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