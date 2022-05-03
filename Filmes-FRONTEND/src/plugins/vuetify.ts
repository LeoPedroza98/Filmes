import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';
import pt from 'vuetify/src/locale/pt';

Vue.use(Vuetify);

export default new Vuetify({
  lang: {
    locales: { pt },
    current: 'pt',
  },
  theme: {
    options:{
      customProperties: true
    },
    dark: true,
    themes: { 
      // light: {
      //   // VUETIFY
      //   primary: '#78909C',
      //   secondary: '#B0BEC5',
      //   accent: '#95A8B2',
      //   error: '#FF0000',
      //   warning: '#FF9800',
      //   info: '#FFD966',
      //   success: '#4CAF50',
      //   background: '#FF00FF',
      //   // CREATED
      //   toolbar: '#ECEFF1'
      // },
      dark: {
        // VUETIFY
        primary: '#252736',
        secondary: '#2F3042',
        accent: '#0177FB',
        error: '#FF7272',
        warning: '#FFBC8C',
        info: '#FD6C22',
        success: '#4CAF50',
        anchor: '#FFFFFF',
        // CREATED
        background: '#1E1D2B',
        toolbar: '#ECEFF1',
        table: '#3B3C52',
        approved:'#4ACFAC',
        notApproved:'#FFAF5F'
      }
    }
  }
});
