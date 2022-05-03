<template>
  <v-app id="app">
    <router-view/>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
});
</script>

<style>

  v-text-field {
    color: white !important;
  }

  .aprovado{
    background-color: var(--v-success-darken2);
  }

  .nao-aprovado{
    background-color: var(--v-warning-darken3);
  }

  .v-tabs-items{
    background-color: var(--v-secondary-base) !important;
  }

  .swal2-title {
    color: white !important;
  }

  .swal2-html-container {
    color: white !important;
  }

  .v-label--active{
    color: white !important;
  }

  .v-data-table{
    background: var(--v-table-base) !important;
  }

  .v-list{
    background: var(--v-background-base) !important;
  }

  .v-list-item__title{
    color: white !important;
  }

  .v-list-item__icon{
    color: white !important;
  }

  .v-list-item--active{
    background-color: var(--v-primary-lighten2) !important;
  }

  .v-card{
    background-color: var(--v-secondary-base) !important;
  }

  .v-stepper {
    background-color: var(--v-secondary-base) !important;
  }

</style>