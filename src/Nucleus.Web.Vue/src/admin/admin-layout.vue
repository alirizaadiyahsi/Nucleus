<template>
    <div>
        <top-menu-component />
        <div class="container-fluid">
            <div class="row">
                <side-menu-component />
                <div class="col-sm-12 col-md-9 mt-3">
                    <router-view></router-view>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="./admin-layout.ts"></script>