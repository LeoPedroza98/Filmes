<template>
    <master-page icone="mdi-card-account-details" titulo="Diretor" subTitulo="Lista de diretores para uso no sistema">
        
        <text-search :search.sync="search" @mudou="Atualizar()"/>
        
        <template v-slot:menu>
            <v-tooltip bottom>
                <template v-slot:activator="{ on }">
                    <v-btn fab small depressed color="primary" class="mr-2" v-on="on" @click="AbrirDialogCadastro()"><v-icon>mdi-plus</v-icon></v-btn>
                </template>
                <span>Novo</span>
            </v-tooltip>
            <v-tooltip bottom>
                <template v-slot:activator="{ on }">
                    <v-btn fab small depressed="" color="primary" class="mr-2" @click="Atualizar()" v-on="on"><v-icon>mdi-refresh</v-icon></v-btn>
                </template>
                <span>Atualizar</span>
            </v-tooltip>
            <v-menu offset-y transition="slide-y-transition">
                <template v-slot:activator="{ on }">
                    <v-btn v-on="on" icon>
                        <v-icon dark>mdi-dots-vertical</v-icon>
                    </v-btn>
                </template>                
                <v-list dense>
                    <v-list-item @click="ExportarExcel('tabela')">
                        <v-list-item-icon><v-icon>mdi-file-excel-outline</v-icon></v-list-item-icon>
                        <v-list-item-title>Exportar (Excel)</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </template>

        <v-data-table id="tabela" :headers="headers" :items="lista" :options.sync="options" :server-items-length="totalLista" :loading="loading" :footer-props="{ showFirstLastPage: true}" class="elevation-1">
            
            <template v-slot:[`item.actions`]="{ item }">
                <v-tooltip right>
                    <template v-slot:activator="{ on }">
                        <v-icon small @click="AbrirDialogCadastro(item)" v-on="on">mdi-pencil</v-icon>
                    </template>
                    <span>Editar</span>
                </v-tooltip>
                <v-tooltip right>
                    <template v-slot:activator="{ on }">
                        <v-icon small @click="Excluir(item)" v-on="on">mdi-delete</v-icon>
                    </template>
                    <span>Excluir</span>
                </v-tooltip>
            </template>

            <template v-slot:[`item.foto`]="{ item }">
                <img v-if="item.foto" :src="`data:image/jpeg;base64,${item.foto}`" class="ma-2" style="max-height: 80px"/>
            </template>

            <template v-slot:[`item.ativo`]="{ item }">
                {{ item.ativo.toSimNao() }}
            </template>

        </v-data-table>

        <cadastro-diretor v-model="dialogCadastro" :item="item" @fechou="dialogCadastro = false" @salvou="Atualizar()"/>

    </master-page>    
</template>

<script lang="ts">
import { Diretor } from "@/core/models/all";
import { PageBase } from "@/core/models/shared";
import { DiretorService } from "@/core/services/geral";
import { Component, Prop, Watch, Vue } from "vue-property-decorator";

@Component
export default class ListaDiretor extends PageBase {
    search: any = '';
    loading: boolean = false;
    dialogCadastro: boolean = false;
    totalLista: number = 0;
    lista: any[] = [];
    options: any = {
        sortBy:['nome'],
        itemsPerPage: 15
    };
    headers: any[] = [
        { text: '',value:'actions' ,sortable: false },
        { text: 'Nome', value: 'nome' },
    ];

    item = new Diretor();
    service = new DiretorService();
    
    foto: any = null;

    @Watch('options', { deep: true })
    Atualizar(){
        const { page, itemsPerPage, sortBy, sortDesc, search, columns } = this.options;
        this.loading = true;
        this.service.Listar(page, itemsPerPage, sortBy, sortDesc, this.search, this.headers).then(
        res => {
            this.lista = res.data.items;
            this.totalLista = res.data.count;
        },
        err => {
            if (!err.response){
                if(!navigator.onLine){
                    this.$swal("Aviso", "Não foi possível se conectar com a internet", "error");
                }
                else{
                    this.$swal("Aviso", "Não foi possível acessar a API", "error");
                }
            }
            else{
                this.$swal("Aviso", err.response.data, "error");
            }
        })
        .finally(() => (this.loading = false));
    }

    AbrirDialogCadastro(item?: Diretor){
        if(item){
            this.service.ObterPorId(item.id).then(
                res=>{
                    this.item = res.data;
                    this.dialogCadastro = true;
                },
                err => {
                    if (!err.response){
                        if(!navigator.onLine){
                            this.$swal("Aviso", "Não foi possível se conectar com a internet", "error");
                        }
                        else{
                            this.$swal("Aviso", "Não foi possível acessar a API", "error");
                        }
                    }
                    else{
                        this.$swal("Aviso", err.response.data, "error");
                    }
                }
            )
        }
        else{
            this.item = new Diretor();
            this.dialogCadastro = true;
        }
    }

    Excluir(item: Diretor){
        this.$swal({    
            title: 'Atenção!',
            text: 'Tem certeza que deseja excluir o registro atual?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            showCloseButton: true,
            showLoaderOnConfirm: true,
            preConfirm: () => {
                return this.service.Excluir(item.id)
                .then(
                    res => {
                        if (res.status == 200){
                            return res.data;
                        }
                    },
                    err => {
                        if (err.response.status == 403){
                            this.$swal("Aviso", "Você não tem permissão para essa funcionalidade!", "warning");                
                        }
                        else{
                            this.$swal('Aviso', err.response.data, 'error')
                        }
                    }
                )
                },
            // @ts-ignore: Unreachable code error
            allowOutsideClick: () => !this.$swal.isLoading()
        })
        .then((result) => {
            if(result.value){
                this.$swal("Aviso", result.value, "success");
                this.Atualizar();
            }
        })
    }    
}
</script>