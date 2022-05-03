import Vue from 'vue';
import { TableToExcel } from '@/assets/scripts/helper';


export class PageBase extends Vue {

    public sessionApp: any;

    public app: { usuarioId: number, usuarioNome: string, role: string, login: string, usuarioFoto: string, temaEscuro: boolean } = {
        usuarioId: 0,
        usuarioNome: '',
        role: '',
        login: '',
        usuarioFoto: '',
        temaEscuro: false
    };

    constructor() {
        super();

        if (!localStorage.sessionApp) {
            this.$router.push({ name: 'Login' });
            return;
        }

        this.sessionApp = JSON.parse(localStorage.sessionApp);

        this.app.usuarioId = this.sessionApp.dados.usuarioId;
        this.app.usuarioNome = this.sessionApp.dados.usuarioNome;
        this.app.role = this.sessionApp.dados.role;
        this.app.login = this.sessionApp.dados.login;
        this.app.usuarioFoto = this.sessionApp.dados.usuarioFoto;
        // this.app.temaEscuro = this.sessionApp.dados.temaEscuro;

    }

    ExportarExcel(tabela: string, worksheet?: string) {
        TableToExcel(tabela, worksheet);
    }

    MudarTema(temaEscuro: boolean) {
        this.app.temaEscuro = temaEscuro;
        this.sessionApp.dados.temaEscuro = temaEscuro;
        localStorage.setItem('sessionApp', JSON.stringify(this.sessionApp));
        this.$vuetify.theme.dark = temaEscuro;
    }
}