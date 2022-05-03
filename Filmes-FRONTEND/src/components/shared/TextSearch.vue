<template>
    <v-text-field v-model="localSearch" append-icon="mdi-magnify" label="Pesquisa rápida" class="mx-1" clearable single-linehide-details @change="Mudou()" filled dense/>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";

@Component
export default class TextSearch extends Vue {
    @Prop() private search!: string;
    
    localSearch: string = '';

    Mudou(){
        this.$emit('update:search', this.localSearch);
        this.$emit("mudou");
    }
}

</script>