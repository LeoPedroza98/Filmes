<template>
  <master-page icone="mdi-home" titulo="Home">
    HOME
  </master-page>
</template>

<script lang="ts">
import { PageBase } from "@/core/models/shared";
import { Vue, Component, Watch } from "vue-property-decorator";

@Component
export default class Home extends PageBase {
  
}
</script>
