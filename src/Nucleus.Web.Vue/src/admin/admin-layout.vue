<template>
  <nav>
    <div x-data="{ sidebarOpen: false }" class="flex h-screen bg-gray-200">
      <AsideMenu/>
      
      <div class="flex-1 flex flex-col overflow-hidden">

        <TopMenu/>
        
        <main class="flex-1 overflow-x-hidden overflow-y-auto bg-gray-200">
          <div class="container mx-auto px-4 py-4">
            <router-view/>
          </div>
        </main>
      </div>
    </div>
  </nav>
</template>

<script src="./admin-layout.ts"></script>