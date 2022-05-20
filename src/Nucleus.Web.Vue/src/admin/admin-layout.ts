import {defineComponent} from "vue";
import AsideMenu from "@/admin/components/menu/aside-menu/aside-menu.vue";
import TopMenu from "@/admin/components/menu/top-menu/top-menu.vue";

export default  defineComponent({
    name:"AdminLayoutComponent",
    components: {
        AsideMenu,
        TopMenu
    },
})