import Vue from "vue";

import AlterarSenha from "./shared/AlterarSenha.vue"
import MasterPage from "./shared/MasterPage.vue";
import TextSearch from "./shared/TextSearch.vue";

import CadastroDiretor from "./all/diretor/Cadastro.vue";
import CadastroFilme from "./all/filme/Cadastro.vue";
import CadastroUsuario from "./all/usuario/Cadastro.vue";
import CadastroFilmeDiretor from "./all/filmeDiretor/Cadastro.vue";

Vue.component("alterar-senha", AlterarSenha);
Vue.component("master-page", MasterPage);
Vue.component("text-search", TextSearch);

Vue.component("cadastro-diretor", CadastroDiretor);
Vue.component("cadastro-filme", CadastroFilme);
Vue.component("cadastro-usuario",CadastroUsuario);
Vue.component("cadastro-filme-diretor",CadastroFilmeDiretor);
